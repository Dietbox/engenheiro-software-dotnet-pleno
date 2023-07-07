

using Dietbox.ECommerce.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dietbox.ECommerce.WebAPI.Configurations.Services
{
    public static class JsonWebTokenServices
    {
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration, bool isDevelopment = false)
        {

            string? key = configuration.GetSection("JsonWebToken:Key").Value;
            byte[] symmetricSecurityKey = Encoding.ASCII.GetBytes(key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricSecurityKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<JsonWebToken>();
        }
    }
}
