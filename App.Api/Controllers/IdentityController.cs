using App.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IUserService _service;


        public IdentityController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(AuthenticateRequest tokenRequest)
        {
            var token = await _service.Authenticate(tokenRequest);

            return Ok(token);
        }
    }
}
