using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Interfaces.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietbox.ECommerce.WebAPI.Controllers
{

    [ApiController]
    [Route("companies")]
    [ApiExplorerSettings(GroupName = "Empresas")]
    public class CompaniesController : BaseController
    {

        private readonly ICompaniesHandler _handler;

        public CompaniesController(ICompaniesHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// [ EndPoint ] Criar conta de empresa.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(CreateCompanyAccountCommand command)
        {
            await _handler.CreateAccount(command);
            return Ok(null, "Empresa cadastrada com êxito.");
        }

        /// <summary>
        /// [ EndPoint ] Criar fazer login da empresa.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCompanyCommand command)
        {
            var result = await _handler.Login(command);
            return Ok(result, "Login da empresa efetuado com êxito.");
        }

    }
}
