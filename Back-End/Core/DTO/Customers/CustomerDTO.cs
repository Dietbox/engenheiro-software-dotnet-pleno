using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.Customers
{
    public class CustomerDTO
    {
        /// <summary>
        /// Identificador do cliente.
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Data/Hora de cadastro do cliente.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
