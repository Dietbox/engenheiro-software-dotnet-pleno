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
        private readonly ICompanyService _companyService;

        public UserController(IUserService userService, ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
        }

        /// <summary>
        /// Registra usuários comuns (Role Default)
        /// </summary>
        /// <returns>Sem Retorno</returns>
        /// <response code="204">Sem Retorno</response>
        /// <response code="400">Usuário já existe no cadatro ou dados do usuário não seguem os requisitos mínimos</response>
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

        /// <summary>
        /// Registra usuário admin (Role admin). Somente outro usuário admin pode registrar um novo admin
        /// </summary>
        /// <returns>Sem Retorno</returns>
        /// <response code="204">Sem Retorno</response>
        /// <response code="400">Usuário já existe no cadatro ou dados do usuário não seguem os requisitos mínimos</response>
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

        /// <summary>
        /// Registra empresa (Role Company). Somente um usuário admin pode registrar uma empresa.
        /// </summary>
        /// <returns>Sem Retorno</returns>
        /// <response code="204">Sem Retorno</response>
        /// <response code="400">Empresa já existe no cadatro ou dados da empresa não seguem os requisitos mínimos</response>
        [HttpPost]
        [Route("registeracompany")]
        public async Task<IActionResult> RegisterCompany([FromBody] CompanyAppRegister companyUserApp)
        {
            if (ModelState.IsValid)
            {
                var companyAdded = await _companyService.CreateUserApp(companyUserApp);

                if (companyAdded)
                    return NoContent();

                BadRequest("Company already exists!");
            }

            return BadRequest(ModelState);
        }
    }
}
