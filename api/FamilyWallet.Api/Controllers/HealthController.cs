using Microsoft.AspNetCore.Mvc;

namespace FamilyWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route(""), HttpGet]
        public string Ping()
        {
            return "Pong";
        }
    }
}
