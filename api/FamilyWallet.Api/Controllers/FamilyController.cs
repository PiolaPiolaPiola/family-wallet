using FamilyWallet.Infraestructure.DTOs;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FamilyWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;

        public FamilyController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] FamilyRequest familyRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _familyService.CreateFamilyAsync(familyRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetFamilies([FromQuery] string? code, [FromQuery] string? codeInvitacion)
        {
            try
            {
                if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(codeInvitacion))
                {
                    var allFamilies = await _familyService.GetAllFamiliesAsync();
                    return Ok(allFamilies);
                }

                var family = await _familyService.GetFamilyAsync(code, codeInvitacion);
                return Ok(family);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
