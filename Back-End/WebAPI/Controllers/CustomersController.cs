using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Commands.Customers;
using Dietbox.ECommerce.Core.Commands.Users;
using Dietbox.ECommerce.Core.Interfaces.Companies;
using Dietbox.ECommerce.Core.Interfaces.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietbox.ECommerce.WebAPI.Controllers
{

    [ApiController]
    [Route("customers")] 
    [ApiExplorerSettings(GroupName = "Usuários")]
    public class CustomersController : BaseController
    {            

        private readonly ICustomersHandler _handler;

        public CustomersController(ICustomersHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// [ EndPoint ] Criar conta de cliente.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(CreateCustomerAccountCommand command)
        {
            await _handler.CreateAccount(command);
            return Ok(null, "Cliente cadastrado com êxito.");
        }

        /// <summary>
        /// [ EndPoint ] Criar fazer login de cliente.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCustomerCommand command)
        {
            var result = await _handler.Login(command);
            return Ok(result, "Login do cliente efetuado com êxito.");
        }

    }
}
