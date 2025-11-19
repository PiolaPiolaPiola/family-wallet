using FamilyWallet.Infraestructure.Data;
using FamilyWallet.Infraestructure.DTOs;
using FamilyWallet.Infraestructure.Entities;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly PostgreSQLContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(PostgreSQLContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

            if (user == null || !await VerifyPasswordAsync(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            // Actualizar último login
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var token = await GenerateTokenAsync(user.Id, user.Email, user.Role);
            var refreshToken = Guid.NewGuid().ToString();

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                User = new UserInfo
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role
                }
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Verificar si el usuario ya existe
            if (await _context.User.AnyAsync(u => u.Email == request.Email))
            {
                throw new InvalidOperationException("El usuario ya existe con este email");
            }

            if (await _context.User.AnyAsync(u => u.Username == request.Username))
            {
                throw new InvalidOperationException("El nombre de usuario ya está en uso");
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = await HashPasswordAsync(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var token = await GenerateTokenAsync(user.Id, user.Email, user.Role);
            var refreshToken = Guid.NewGuid().ToString();

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                User = new UserInfo
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role
                }
            };
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"] ?? "Q10TaskManager",
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtSettings:Audience"] ?? "Q10TaskManagerUsers",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GenerateTokenAsync(int userId, string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!");

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role),
                new("sub", userId.ToString()),
                new("jti", Guid.NewGuid().ToString()),
                new("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = _configuration["JwtSettings:Issuer"] ?? "Q10TaskManager",
                Audience = _configuration["JwtSettings:Audience"] ?? "Q10TaskManagerUsers",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        public async Task<bool> VerifyPasswordAsync(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
