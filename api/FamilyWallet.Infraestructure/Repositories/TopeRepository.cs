using FamilyWallet.Infraestructure.Data;
using FamilyWallet.Infraestructure.Entities;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyWallet.Infraestructure.Repositories
{
    public class TopeRepository : ITopeRepository
    {
        public PostgreSQLContext Context { get; set; }

        public TopeRepository(PostgreSQLContext context)
        {
            Context = context;
        }

        public async Task<Tope> CreateAsync(Tope tope)
        {
            await Context.Topes.AddAsync(tope);
            await Context.SaveChangesAsync();
            return tope;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var tope = await GetByIdAsync(id);
            if (tope == null) return false;
            Context.Topes.Remove(tope);
            await Context.SaveChangesAsync();
            return true;
        }

        public Task<IQueryable<Tope>> GetAllAsync()
        {
            var topes = Context.Topes.AsQueryable();
            return Task.FromResult(topes);
        }

        public async Task<Tope?> GetByIdAsync(string id)
        {
            var tope = await Context.Topes
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            return tope;
        }

        public async Task<Tope> UpdateAsync(string id, Tope tope)
        {
            var topeExistente = await Context.Topes.FindAsync(id);
            if (topeExistente == null)
                throw new KeyNotFoundException($"No se encontró el Tope con id '{id}'.");

            // Actualiza solo los campos necesarios
            topeExistente.Anio = tope.Anio;
            topeExistente.Mes = tope.Mes;
            topeExistente.ValorMaximo = tope.ValorMaximo;
            topeExistente.ValorMinimo = tope.ValorMinimo;
            topeExistente.CodigoPersona = topeExistente.CodigoPersona;

            await Context.SaveChangesAsync();
            return topeExistente;
        }
    }
}