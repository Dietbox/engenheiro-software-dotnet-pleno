using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Customers;
using Dietbox.ECommerce.ORM.Entities.Products;
using Dietbox.ECommerce.ORM.Entities.Users;
using Dietbox.ECommerce.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Contexts
{
    public class BasicContext : DbContext
    {
        
        private readonly IServiceProvider _services;
        public BasicContext(DbContextOptions<BasicContext> options, IServiceProvider services) : base(options)
        {
            _services = services;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetConfigurations(modelBuilder);   
            modelBuilder?.Entity<Company>();
            modelBuilder?.Entity<Customer>();
            modelBuilder?.Entity<Product>();
            modelBuilder?.Entity<Order>();
        }

        protected void SetConfigurations(ModelBuilder modelBuilder)
        {
            IEnumerable<IEntityConfiguration> services = _services.GetServices<IEntityConfiguration>();
            foreach (IEntityConfiguration service in services) { service.Configure(modelBuilder); }
        }

    }
}
