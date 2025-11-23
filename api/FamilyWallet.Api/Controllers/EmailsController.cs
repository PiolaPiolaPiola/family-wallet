using FamilyWallet.Infraestructure.DTOs.Email;
using FamilyWallet.Infraestructure.Repositories.Interfaces.Messages;
using Microsoft.AspNetCore.Mvc;

namespace FamilyWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailBulkCommandService _commandService;

        public EmailsController(IEmailBulkCommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmails([FromBody] EmailRequestDto request)
        {
            foreach (var email in request.To)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("La lista de correos electrónicos no debe contener entradas vacías.");
                }

                if (!IsValidEmail(email))
                {
                    return BadRequest("Uno o más correos electrónicos no son válidos.");
                }
            }

            try
            {
                var result = await _commandService.ProcessBulkEmailAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing email: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
