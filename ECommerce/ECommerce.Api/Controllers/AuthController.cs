using ECommerce.Domain.Models;
using ECommerce.Infra.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] User user, [FromServices] AccessManager accessManager)
        {
            if (await accessManager.ValidarCredenciaisAsync(user))
                return Ok(accessManager.GerarToken(user));

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] User userDto, [FromServices] AccessManager accessManager)
        {
            var ok = await accessManager.CriarUsuario(userDto.Email);

            if (ok)
            {
                return Ok();
            }
            
            return BadRequest();
        }
    }
}
