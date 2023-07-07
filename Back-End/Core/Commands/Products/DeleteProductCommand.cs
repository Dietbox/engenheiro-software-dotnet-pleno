using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Commands.Products
{
    public class DeleteProductCommand : Command
    {

        /// <summary>
        /// Identificador do produto.
        /// </summary>
        public int ID { get; set; }

    }
}
