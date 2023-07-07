using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.DTO.API;
using Dietbox.ECommerce.Core.DTO.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces.Companies
{
    public interface ICompaniesHandler
    {

        /// <summary>
        /// Criar conta de empresa.
        /// </summary>
        /// <param name="command">Comando de criação da empresa.</param>
        Task CreateAccount(CreateCompanyAccountCommand command);

        /// <summary>
        /// Realiza o login da empresa.
        /// </summary>
        /// <param name="command">Comando de login.</param>
        /// <returns>Retorna um objeto do tipo 'AuthorizationDTO' contendo o token JWT.</returns>
        Task<AuthorizationDTO> Login(LoginCompanyCommand command);

    }
}
