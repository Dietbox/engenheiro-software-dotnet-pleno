using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.ORM;
using Dietbox.ECommerce.ORM.Contexts;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Customers;
using Dietbox.ECommerce.ORM.Entities.Products;
using Dietbox.ECommerce.ORM.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace Dietbox.ECommerce.WebAPI.Configurations.Services
{

    public static class ORMServices
    {
        public static void RegisterServicesForORM(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(x => services.GetType().Assembly);
            //services.UseBasicContextTo("Server=localhost\\SQLEXPRESS; Database=master; Trusted_Connection=True;");

            string currentDirectory = Directory.GetCurrentDirectory();
            string directory = Directory.GetParent(currentDirectory).FullName + "\\Core";
            string attachDbFilename = directory + "\\DataBase.mdf";

            services.UseBasicContextTo($"Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename={attachDbFilename}; Integrated Security=True");
            //services.UseBasicContextTo("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Leonardo\\Desktop\\Dietbox-eCommerce\\Back-End\\Core\\DataBase.mdf;Integrated Security=True");
            services.UseAllConfigurations();

            // Entidades:
            services.UseReposityTo<Company>();
            services.UseReposityTo<Customer>();
            services.UseReposityTo<Product>();
            services.UseReposityTo<Order>();

        }
    }

}
