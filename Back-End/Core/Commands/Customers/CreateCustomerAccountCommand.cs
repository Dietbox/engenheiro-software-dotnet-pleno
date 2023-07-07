using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Commands.Users
{
    public class CreateCustomerAccountCommand : Command
    {

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail de cadastro do  usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha de acesso do usuário.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Token do Google Recaptcha (utilizado porque API é pública).
        /// </summary>
        public string Recaptcha { get; set; }

    }
}
