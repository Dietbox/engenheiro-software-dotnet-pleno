using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Commands.Products
{
    public class BuyProductCommand:Command
    {
        public int ID { get; set; }
        public int Amount { get; set; }
    }
}
