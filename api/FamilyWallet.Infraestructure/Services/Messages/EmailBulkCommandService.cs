using FamilyWallet.Infraestructure.DTOs.Email;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using FamilyWallet.Infraestructure.Repositories.Interfaces.Messages;

namespace FamilyWallet.Infraestructure.Services
{
    public class EmailBulkCommandService : IEmailBulkCommandService
    {
        private readonly IRabbitMQRepository _rabbitMQRepository;

        public EmailBulkCommandService(IRabbitMQRepository rabbitMQRepository)
        {
            _rabbitMQRepository = rabbitMQRepository;
        }

        public async Task<string> ProcessBulkEmailAsync(EmailRequestDto email)
        {

            var command = new EmailBulkCommand
            {
                Email = email
            };

            await _rabbitMQRepository.PublishAsync(command, "email-bulk-queue");

            return command.Id;
        }
    }
}
