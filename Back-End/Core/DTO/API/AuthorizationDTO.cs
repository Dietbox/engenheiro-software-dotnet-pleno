using Dietbox.ECommerce.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.API
{
    public class AuthorizationDTO
    {
        public AuthorizationDTO(string token, DateTime expiration, string name, TenantType type)
        {
            Token = token;
            Expiration = expiration;
            Name = name;
            Type = type;
        }

        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Name{ get; set; }
        public TenantType Type { get; set; }
    }
}
