using FamilyWallet.Infraestructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FamilyWallet.Infraestructure.Services.Messages
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailsAsync(List<string> emails, string subject, string body)
        {
            var sendGridApiKey = _configuration["SendGridSettings:ApiKey"];
            var fromEmail = _configuration["SendGridSettings:FromEmail"];
            var fromName = _configuration["SendGridSettings:FromName"];

            try
            {
                // Crea el cliente de SendGrid
                var client = new SendGridClient(sendGridApiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var plainTextContent = body;
                var htmlContent = body;
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emails.ConvertAll(email => new EmailAddress(email)), subject, plainTextContent, htmlContent);

                // Enviar el correo
                var response = await client.SendEmailAsync(msg);

                // Verifica si se envió correctamente
                return response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
