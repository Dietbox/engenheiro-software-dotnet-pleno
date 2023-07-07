using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Commands.Customers;
using Dietbox.ECommerce.Core.Commands.Users;
using Dietbox.ECommerce.Core.DTO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces.Customers
{
    public interface ICustomersHandler
    {

        /// <summary>
        /// Criar conta de cliente.
        /// </summary>
        /// <param name="command">Comando de criação do cliente.</param>
        Task CreateAccount(CreateCustomerAccountCommand command);


        /// <summary>
        /// Realiza o login do cliente.
        /// </summary>
        /// <param name="command">Comando de login.</param>
        /// <returns>Retorna um objeto do tipo 'AuthorizationDTO' contendo o token JWT.</returns>
        Task<AuthorizationDTO> Login(LoginCustomerCommand command);

    }
}
