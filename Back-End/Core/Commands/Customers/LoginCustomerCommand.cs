using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Commands.Customers
{
    public class LoginCustomerCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
