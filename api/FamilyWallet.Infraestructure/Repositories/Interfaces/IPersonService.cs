using FamilyWallet.Infraestructure.DTOs;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface IPersonService
    {
        Task<PersonResponse> CreatePersonAsync(PersonRequest person);

        Task<PersonResponse> GetPersonAsync(string code);

        Task<List<PersonResponse>> GetAllPersonsByFamilyCodeAsync(string familyCode);

        Task<PersonResponse> UpdatePersonAsync(string code, PersonRequest personRequest);

        Task DeletePersonAsync(string code);
    }
}
