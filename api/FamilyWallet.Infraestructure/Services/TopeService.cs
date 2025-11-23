using FamilyWallet.Infraestructure.Data;
using FamilyWallet.Infraestructure.DTOs;
using FamilyWallet.Infraestructure.DTOs.Tope;
using FamilyWallet.Infraestructure.Entities;
using FamilyWallet.Infraestructure.Repositories.Interfaces;

namespace FamilyWallet.Infraestructure.Services
{
    public class TopeService : ITopeService
    {
        public ITopeRepository TopeRepository { get; set; }

        public TopeService(ITopeRepository topeRepository)
        {
            TopeRepository = topeRepository;
        }

        public async Task<Tope> Create(TopeCreateDto topeCreateDTO)
        {
            var tope = new Tope()
            {
                Mes = topeCreateDTO.Mes,
                Anio = topeCreateDTO.Anio,
                ValorMinimo = topeCreateDTO.ValorMinimo,
                ValorMaximo = topeCreateDTO.ValorMaximo,
                CodigoPersona = topeCreateDTO.CodigoPersona
            };

            var newTope = await TopeRepository.CreateAsync(tope);
            return newTope;
        }

        public async Task<bool> Delete(string id)
        {
            return await TopeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Tope>> GetAll()
        {
            return await TopeRepository.GetAllAsync();
        }

        public async Task<Tope> GetById(string id)
        {
            return await TopeRepository.GetByIdAsync(id);
        }

        public async Task<Tope> Update(string id, TopeUpdateDto topeUpdateDTO)
        {
            var tope = new Tope()
            {
                Mes = topeUpdateDTO.Mes,
                Anio = topeUpdateDTO.Anio,
                ValorMinimo = topeUpdateDTO.ValorMinimo,
                ValorMaximo = topeUpdateDTO.ValorMaximo
            };

            var updatedTope = await TopeRepository.UpdateAsync(id, tope);
            return updatedTope;
        }
    }
}