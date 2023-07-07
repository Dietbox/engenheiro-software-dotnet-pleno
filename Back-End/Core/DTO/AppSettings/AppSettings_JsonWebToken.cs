using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.AppSettings
{
    public class AppSettings_JsonWebToken
    {
        public string Key { get; set; }
        public int HoursToExpire { get; set; }
    }
}
