using FamilyWallet.Infraestructure.DTOs.Email;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface ISendEmailBulkCommandService
    {
        Task<List<string>> ProcessBulkTasksAsync(EmailBulkRequest emails);
    }
}