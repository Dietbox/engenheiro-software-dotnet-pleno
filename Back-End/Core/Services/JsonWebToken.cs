using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Users;
using Dietbox.ECommerce.Tenant;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Services
{
    public class JsonWebToken
    {

        private readonly ISettings _settings;
        public JsonWebToken(ISettings settings)
        {
            _settings = settings;
        }

        public (string, DateTime?) GenerateToken(Customer customer)
        {
            return _generate(customer.ID, customer.Name, customer.Email, TenantType.Customer);
        }

        public (string, DateTime?) GenerateToken(Company company)
        {
            return _generate(company.ID, company.Name, company.Email, TenantType.Company);
        }

        private (string, DateTime?) _generate(int id, string name, string email, TenantType type)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] buffer = Encoding.ASCII.GetBytes(_settings.JWT.Key);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("ID", id.ToString()),
                    new Claim("Name", name),
                    new Claim("Email", email),
                    new Claim("Type", ((int)type).ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_settings.JWT.HoursToExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(buffer), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);
            return (token, tokenDescriptor.Expires);
        }
    }
}
