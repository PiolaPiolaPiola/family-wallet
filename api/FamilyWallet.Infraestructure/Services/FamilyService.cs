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
    public class FamilyService : IFamilyService
    {
        private readonly PostgreSQLContext _context;
        private readonly IConfiguration _configuration;

        public FamilyService(PostgreSQLContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<FamilyResponse> CreateFamilyAsync(FamilyRequest FamilyRequest)
        {
            var family = new Family
            {
                Name = FamilyRequest.Name,
                Status = true,
                CodeInvitation =  "code_invitation/#" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper() //TO DO: Personalizar la cración del código
            };

            _context.Family.Add(family);
            await _context.SaveChangesAsync();

            var familyResponse = new FamilyResponse()
            {
                Code = family.Code,
                Name = family.Name,
                Status = family.Status,
                CodeInvitation = family.CodeInvitation,
            };

            return familyResponse;
        }

        public async Task<FamilyResponse> GetFamilyAsync(string? code, string? codeInvitacion)
        {
            var family = await _context.Family.FirstOrDefaultAsync(f =>
                (code != null && f.Code == code) ||
                (codeInvitacion != null && f.CodeInvitation == codeInvitacion)
            );

            if (family == null) throw new Exception("No se encontró la familia con los parámetros ingresados");

            return new FamilyResponse()
            {
                Code = family.Code,
                Name = family.Name,
                Status = family.Status,
                CodeInvitation = family.CodeInvitation
            };
        }

        public async Task<List<FamilyResponse>> GetAllFamiliesAsync()
        {
            return await _context.Family
                .Select(f => new FamilyResponse
                {
                    Code = f.Code,
                    Name = f.Name,
                    Status = f.Status,
                    CodeInvitation = f.CodeInvitation
                })
                .ToListAsync();
        }
    }
}
