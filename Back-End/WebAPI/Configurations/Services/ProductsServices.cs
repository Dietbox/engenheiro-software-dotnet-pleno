using Dietbox.ECommerce.Core.Handlers.Products;
using Dietbox.ECommerce.Core.Interfaces.Products;
using Dietbox.ECommerce.Core.Mappers.Products;
using Dietbox.ECommerce.Core.Queries.Products;
using Dietbox.ECommerce.Core.Validations.Companies;
using Dietbox.ECommerce.Core.Validations.Products;

namespace Dietbox.ECommerce.WebAPI.Configurations.Services
{
    public static class ProductsServices
    {
        public static void RegisterServicesForProducts(this IServiceCollection services)
        {
            // Mappers
            services.AddScoped<ProductMapper>();
            services.AddScoped<ProductMapperConfiguration>();

            // Handler & Queries:
            services.AddScoped<IProductsHandler, ProductsHandler>();
            services.AddScoped<IProductsQueries, ProductsQueries>();

            // Validators:
            services.AddScoped<CreateProductValidator>();
            services.AddScoped<BuyProductValidator>();

        }
    }
}
