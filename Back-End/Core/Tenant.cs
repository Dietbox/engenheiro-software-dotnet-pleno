using Dietbox.ECommerce.Tenant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core
{
    public class Tenant : ITenant
    {
        public int ID { get; }
        public string Name { get; }
        public string Email { get; }
        public TenantType Type { get; }

        private readonly IHttpContextAccessor _accessor;

        public Tenant(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            ID = int.Parse(_getClaim("ID"));
            Name = _getClaim("Name");
            Email = _getClaim("Email");
            Type = Enum.Parse<TenantType>(_getClaim("Type"));
        }

        string _getClaim(string key)
        {
            IEnumerable<Claim> claims = _accessor.HttpContext.User.Claims;
            string? value = claims.Where(c => c.Type == key)
                .Select(x => x.Value).FirstOrDefault();
            return value;
        }
    }
}
