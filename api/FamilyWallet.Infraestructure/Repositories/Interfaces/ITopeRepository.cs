using FamilyWallet.Infraestructure.DTOs;
using FamilyWallet.Infraestructure.DTOs.Tope;
using FamilyWallet.Infraestructure.Entities;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface ITopeRepository
    {
        Task<IQueryable<Tope>> GetAllAsync();
        Task<Tope?> GetByIdAsync(string id);
        Task<Tope> CreateAsync(Tope tope);
        Task<Tope> UpdateAsync(string id, Tope tope);
        Task<bool> DeleteAsync(string id);
    }
}