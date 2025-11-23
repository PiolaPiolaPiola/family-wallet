using FamilyWallet.Infraestructure.DTOs.Email;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using FamilyWallet.Infraestructure.Repositories.Interfaces.Messages;

namespace FamilyWallet.Infraestructure.Services
{
    public class ProcessBulkService : IProcessBulkService
    {
        private readonly IEmailService _emailService;
        private readonly IRabbitMQRepository _rabbitMQRepository;

        public ProcessBulkService(IEmailService emailService, IRabbitMQRepository rabbitMQRepository)
        {
            _emailService = emailService;
            _rabbitMQRepository = rabbitMQRepository;
        }

        public async Task StartConsumingAsync()
        {
            await _rabbitMQRepository.StartConsumingAsync<EmailBulkCommand>(
                "email-bulk-queue",
                ProcessBulkCommand);
        }

        public async Task ProcessBulkCommand(EmailBulkCommand command)
        {
            var resultadoServicio = await _emailService.SendEmailsAsync(command.Email.To, command.Email.Titulo, command.Email.Descripcion);
            var resultadoCola = new EmailBulkResponse();

            if (resultadoServicio)
            {
                new EmailBulkResponse
                {
                    Title = "Envio recibido",
                    IsSuccess = true
                };
            }
            else
            {
                new EmailBulkResponse
                {
                    Title = "Envio recibido sin éxito",
                    IsSuccess = true
                };
            }
        }
    }
}
