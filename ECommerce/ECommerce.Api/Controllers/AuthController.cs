using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : Controller
    {
        private readonly IAccessManager _accessManager;

        public AuthController(IAccessManager accessManager)
        {
            _accessManager = accessManager;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (await _accessManager.ValidateCredentials(user))
                return Ok(_accessManager.GenerateToken(user));

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                var userCreated = await _accessManager.CreateUser(user);

                if (userCreated)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("logout")]
        [Produces("application/json")]
        public IActionResult LogOut()
        {
            _accessManager.DeactivateCurrentAsync();

            return NoContent();
        }

    }
}
