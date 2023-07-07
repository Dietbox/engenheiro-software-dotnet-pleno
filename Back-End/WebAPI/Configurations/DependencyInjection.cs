using Dietbox.ECommerce.Core;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Core.Services;
using Dietbox.ECommerce.Core.Validations;
using Dietbox.ECommerce.Tenant;
using Dietbox.ECommerce.WebAPI.Configurations.Services;

namespace Dietbox.ECommerce.WebAPI.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {                    
            services.AddScoped<ICommandValidator, CommandValidator>();
            services.AddScoped<ISettings, AppSettings>();
            services.AddScoped<ITenant, Dietbox.ECommerce.Core.Tenant>();
            services.AddScoped<GoogleRecaptcha>();

            services.AddSwagger(configuration, environment);
            services.AddJWT(configuration);

            services.RegisterServicesForORM(configuration);
            services.RegisterServicesForCompanies();
            services.RegisterServicesForCustomers();
            services.RegisterServicesForProducts();
        }
    }
}
