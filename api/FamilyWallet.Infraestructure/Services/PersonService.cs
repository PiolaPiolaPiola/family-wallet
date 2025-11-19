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
    public class PersonService : IPersonService
    {
        private readonly PostgreSQLContext _context;
        private readonly IConfiguration _configuration;

        public PersonService(PostgreSQLContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<PersonResponse> CreatePersonAsync(PersonRequest PersonRequest)
        {
            
            var person = new Person
            {
                Name = PersonRequest.Name,
                Identification = PersonRequest.Identification,
                FamilyCode = PersonRequest.FamilyCode,
                Email = PersonRequest.Email,
                Phone = PersonRequest.Phone,
                Relation = PersonRequest.Relation
            };

            var exists = await _context.Person.AnyAsync(p => p.FamilyCode == person.FamilyCode);

            person.IsMain = !exists;

            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            var personResponse = new PersonResponse()
            {
                Code = person.Code,
                Name = person.Name,
                Identification = person.Identification,
                FamilyCode = person.FamilyCode,
                Email = person.Email,
                Phone = person.Phone,
                Relation = person.Relation,
                IsMain = person.IsMain
            };

            return personResponse;
        }

        public async Task<PersonResponse> GetPersonAsync(string code)
        {
            var person = await _context.Person.FirstOrDefaultAsync(f => f.Code == code);

            if (person == null) { throw new Exception($"No se encontró la persona con el código ingresado"); }

            var personResponse = new PersonResponse()
            {
                Code = person.Code ?? code,
                Name = person.Name,
                Identification = person.Identification,
                FamilyCode = person.FamilyCode,
                Email = person.Email,
                Phone = person.Phone,
                Relation = person.Relation,
                IsMain = person.IsMain
            };

            return personResponse;
        }

        public async Task<List<PersonResponse>> GetAllPersonsByFamilyCodeAsync(string familyCode)
        {
            var personResponses = await _context.Person
                 .Where(p => p.FamilyCode == familyCode)
                 .Select(p => new PersonResponse
                 {
                     Code = p.Code,
                     Name = p.Name,
                     Identification = p.Identification,
                     FamilyCode = p.FamilyCode,
                     Email = p.Email,
                     Phone = p.Phone,
                     Relation = p.Relation,
                     IsMain = p.IsMain
                 })
                 .ToListAsync();

            return personResponses;
        }

        public async Task<PersonResponse> UpdatePersonAsync(string code, PersonRequest personRequest)
        {
            var personInformation = await _context.Person.FirstOrDefaultAsync(f => f.Code == code);

            if (personInformation == null) { throw new Exception($"No se encontró la persona con el código ingresado"); }

            personInformation.Name = personRequest.Name;
            personInformation.Identification = personRequest.Identification;
            personInformation.FamilyCode = personRequest.FamilyCode;
            personInformation.Email = personRequest.Email;
            personInformation.Phone = personRequest.Phone;
            personInformation.Relation = personRequest.Relation;

            await _context.SaveChangesAsync();

            personInformation = await _context.Person.FirstOrDefaultAsync(f => f.Code == code);

            var personResponse = new PersonResponse();

            if (personInformation != null) 
            {
                personResponse.Code = personInformation.Code;
                personResponse.Name = personInformation.Name;
                personResponse.Identification = personInformation.Identification;
                personResponse.FamilyCode = personInformation.FamilyCode;
                personResponse.Email = personInformation.Email;
                personResponse.Phone = personInformation.Phone;
                personResponse.Relation = personInformation.Relation;
                personResponse.IsMain = personInformation.IsMain;
            }

            return personResponse;
        }

        public async Task DeletePersonAsync(string code)
        {
            var person = await _context.Person.FirstOrDefaultAsync(f => f.Code == code);

            if (person == null) { throw new Exception($"No se encontró la persona con el código ingresado"); }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
