using Dietbox.ECommerce.Core.DTO.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces
{
    public interface ISettings
    {

        /// <summary>
        /// Diretório atual onde a aplicação está rodando no servidor.
        /// </summary>
        public string CurrentDirectory { get; }

        /// <summary>
        /// Nome do ambiente.
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// URL Base da API.
        /// </summary>
        public string CurrentBaseURL { get; }

        /// <summary>
        /// Cadeia de conexão com banco de dados.
        /// </summary>
        public string ConnectionString { get; }


        /// <summary>
        /// Configurações do Google Recaptcha.
        /// </summary>
        public AppSettings_GoogleRecaptcha Recaptcha { get; }

        /// <summary>
        /// Configurações do Json Web Token.
        /// </summary>
        public AppSettings_JsonWebToken JWT { get; }


    }
}
