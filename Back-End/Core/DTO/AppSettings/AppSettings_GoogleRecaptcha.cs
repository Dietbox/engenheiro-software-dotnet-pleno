using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.AppSettings
{
    public class AppSettings_GoogleRecaptcha
    {

        /// <summary>
        /// Flag indicando se a aplicação vai habilitar a verificação do Google Recaptcha.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// API de verificação do Google Recaptcha.
        /// </summary>
        public string API { get; set; }

        /// <summary>
        /// Segredo do Recaptcha vinculado à essa aplicação.
        /// </summary>
        public string Secret { get; set; }


    }
}
