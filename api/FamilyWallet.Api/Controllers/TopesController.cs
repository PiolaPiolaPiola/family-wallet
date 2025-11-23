using FamilyWallet.Infraestructure.DTOs.Tope;
using FamilyWallet.Infraestructure.Entities;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopesController : ControllerBase
    {
        public ITopeService TopeService { get; set; }

        public TopesController(ITopeService topeService)
        {
            TopeService = topeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Tope> topes;

            try
            {
                topes = await TopeService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(topes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Tope tope;

            try
            {
                tope = await TopeService.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(tope);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TopeCreateDto tope)
        {
            Tope createdTope;
            try
            {
                createdTope = await TopeService.Create(tope);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(createdTope);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TopeUpdateDto tope)
        {
            Tope updatedTope;

            try
            {
                updatedTope = await TopeService.Update(id, tope);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(updatedTope);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isDeleted;

            try
            {
                isDeleted = await TopeService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(isDeleted);
        }
    }
}