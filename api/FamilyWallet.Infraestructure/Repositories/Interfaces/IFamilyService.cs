using FamilyWallet.Infraestructure.DTOs;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface IFamilyService
    {
        Task<FamilyResponse> CreateFamilyAsync(FamilyRequest family);

        Task<FamilyResponse> GetFamilyAsync(string? code = null, string? codeInvitacion = null);

        Task<List<FamilyResponse>> GetAllFamiliesAsync();
    }
}
