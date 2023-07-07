

namespace Dietbox.ECommerce.Core.Commands.Companies
{
    public class CreateCompanyAccountCommand : Command
    {
        /// <summary>
        /// Nome da empresa.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CNPJ da empresa.
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// E-mail de cadastro da empresa.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha de acesso da empresa.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Token do Google Recaptcha (utilizado porque API é pública).
        /// </summary>
        public string Recaptcha { get; set; }
    }
}
