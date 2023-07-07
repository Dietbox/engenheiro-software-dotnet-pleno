using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Commands.Companies
{
    public class LoginCompanyCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
