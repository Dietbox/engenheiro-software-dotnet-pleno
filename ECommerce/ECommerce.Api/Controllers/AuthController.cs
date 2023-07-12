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

        /// <summary>
        /// Faz login utilizando usuário e senha. Token JWT é fornecido
        /// </summary>
        /// <returns>Token JWT</returns>
        /// <response code="200">Retorna Token JWT e outras informações</response>
        /// <response code="401">Usuário ou senha inválidos.</response>
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

        /// <summary>
        /// Faz logoff da aplicação.
        /// </summary>
        /// <returns>Sem retorno</returns>
        /// <response code="201">Sem retorno</response>
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
