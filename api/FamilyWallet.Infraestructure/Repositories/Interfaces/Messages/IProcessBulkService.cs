using FamilyWallet.Infraestructure.DTOs.Email;

namespace FamilyWallet.Infraestructure.Repositories.Interfaces.Messages
{
    public interface IProcessBulkService
    {
        Task ProcessBulkCommand(EmailBulkCommand command);
        Task StartConsumingAsync();
    }
}
