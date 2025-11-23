namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailsAsync(List<string?>? emails, string? subject, string? body);
    }
}