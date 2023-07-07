using Dietbox.ECommerce.Core.Handlers.Companies;
using Dietbox.ECommerce.Core.Interfaces.Companies;
using Dietbox.ECommerce.Core.Mappers.Companies;
using Dietbox.ECommerce.Core.Validations.Companies;

namespace Dietbox.ECommerce.WebAPI.Configurations.Services
{
    public static class CompaniesServices
    {

        public static void RegisterServicesForCompanies(this IServiceCollection services)
        {
            // Mappers
            services.AddScoped<CompanyMapper>();
            services.AddScoped<CompanyMapperConfiguration>();

            // Handler:
            services.AddScoped<ICompaniesHandler, CompaniesHandler>();

            // Validators:
            services.AddScoped<CreateCompanyAccountValidator>();
            services.AddScoped<LoginCompanyValidator>();
        }



    }
}
