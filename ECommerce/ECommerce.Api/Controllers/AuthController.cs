using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Models;
using ECommerce.Infra.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
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
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] User userDto)
        {
            var userCreated = await _accessManager.CreateUser(userDto.Email);

            if (userCreated)
                return Ok();
            
            return BadRequest();
        }
    }
}
