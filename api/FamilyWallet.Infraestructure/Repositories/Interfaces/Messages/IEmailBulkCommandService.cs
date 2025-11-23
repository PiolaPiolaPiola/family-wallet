using FamilyWallet.Infraestructure.DTOs.Email;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces.Messages
{
    public interface IEmailBulkCommandService
    {
        Task<string> ProcessBulkEmailAsync(EmailRequestDto email);
    }
}
