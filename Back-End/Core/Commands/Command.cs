using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Commands
{
    public class Command
    {
        public Command()
        {         
            Time = DateTime.Now;
            TypeName = GetType().Name;
        }
        public DateTime Time { get; set; }
        public string TypeName { get; set; }
    }
}
