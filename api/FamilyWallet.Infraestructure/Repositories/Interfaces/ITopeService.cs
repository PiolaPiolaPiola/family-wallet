using FamilyWallet.Infraestructure.DTOs;
using FamilyWallet.Infraestructure.DTOs.Tope;
using FamilyWallet.Infraestructure.Entities;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface ITopeService
    {
        Task<IEnumerable<Tope>> GetAll();
        Task<Tope> GetById(string id);
        Task<Tope> Create(TopeCreateDto tope);
        Task<Tope> Update(string id, TopeUpdateDto tope);
        Task<bool> Delete(string id);
    }
}