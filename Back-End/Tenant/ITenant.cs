using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Tenant
{
    public interface ITenant
    {

        /// <summary>
        /// Identificador da entidade autenticada.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Nome da entidade autenticada.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// E-mail da entidade autenticada.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Tipo de entidade autenticada (empresa ou cliente).
        /// </summary>
        public TenantType Type { get; }


    }
}
