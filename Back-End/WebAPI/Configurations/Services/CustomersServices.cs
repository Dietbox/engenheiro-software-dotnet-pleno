using Dietbox.ECommerce.Core.Handlers.Customers;
using Dietbox.ECommerce.Core.Interfaces.Customers;
using Dietbox.ECommerce.Core.Mappers.Customers;
using Dietbox.ECommerce.Core.Validations.Customers;

namespace Dietbox.ECommerce.WebAPI.Configurations.Services
{
    public static class CustomersServices
    {

        public static void RegisterServicesForCustomers(this IServiceCollection services)
        {
            // Mappers
            services.AddScoped<CustomerMapper>();
            services.AddScoped<CustomerMapperConfiguration>();

            // Handler:
            services.AddScoped<ICustomersHandler, CustomersHandler>();

            // Validators:
            services.AddScoped<CreateCustomerAccountValidator>();
            services.AddScoped<LoginCustomerValidator>();

        }

    }

   
}
