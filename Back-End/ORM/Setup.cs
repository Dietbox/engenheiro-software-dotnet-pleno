using Dietbox.ECommerce.ORM.Contexts;
using Dietbox.ECommerce.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM
{

    public static class Setup
    {

        public static void UseReposityTo<TEntity>(this IServiceCollection services) where TEntity : class, IIdentity
        {
            services.AddScoped<IRepository<TEntity>, Repository<TEntity>>();
        }


        public static void UseBasicContextTo(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BasicContext>(options =>
            {
                options
                    .UseSqlServer(connectionString)
                    .UseLazyLoadingProxies();

            });
            services.AddScoped<BasicContext>();
        }

        public static void UseAllConfigurations(this IServiceCollection services)
        {
            IEnumerable<Type> types = typeof(Setup).Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.Name == typeof(IEntityConfiguration).Name));
            foreach (Type type in types) { services.AddScoped(typeof(IEntityConfiguration), type); }
        }
    }

}
