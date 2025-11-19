using FamilyWallet.Infraestructure.DTOs;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] PersonRequest personRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _personService.CreatePersonAsync(personRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            try
            {
                var response = await _personService.GetPersonAsync(code);

                if (response == null) return NotFound(new { message = "No se encontró la persona con el código ingresado" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPersonsByFamily([FromQuery] string familyCode)
        {
            if (string.IsNullOrWhiteSpace(familyCode)) return BadRequest(new { error = "El código de familia es obligatorio" });

            try
            {
                var response = await _personService.GetAllPersonsByFamilyCodeAsync(familyCode);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update([FromRoute] string code, [FromBody] PersonRequest personRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _personService.UpdatePersonAsync(code, personRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete([FromQuery] string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code)) return BadRequest(new { error = "El código de persona es obligatorio" });

                await _personService.DeletePersonAsync(code);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
