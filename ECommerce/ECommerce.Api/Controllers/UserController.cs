using ECommerce.Domain.Auth;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_ADMIN)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] UserAppRegister userApp)
        {
            if (ModelState.IsValid)
            {
                var userAdded = await _userService.CreateUserApp(userApp);

                if (userAdded)
                    return NoContent();

                BadRequest("User already exists!");
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserAppRegister userApp)
        {
            if (ModelState.IsValid)
            {
                var userAdded = await _userService.CreateUserApp(userApp, Roles.ROLE_ADMIN);

                if (userAdded)
                    return NoContent();

                BadRequest("User already exists!");
            }

            return BadRequest(ModelState);
        }
    }
}
