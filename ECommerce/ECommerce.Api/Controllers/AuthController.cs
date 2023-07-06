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
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var currentUser = await _accessManager.ValidateCredentials(user);

            if (currentUser is not null)
                return Ok(await _accessManager.GenerateToken(currentUser));

            return Unauthorized();
        }

        [HttpPost]
        [Route("logout")]
        [Produces("application/json")]
        public IActionResult LogOut()
        {
            _accessManager.DeactivateCurrent(User.Identity.Name);

            return NoContent();
        }
    }
}
