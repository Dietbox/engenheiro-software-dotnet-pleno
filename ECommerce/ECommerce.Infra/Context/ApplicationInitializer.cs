using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Infra.Context
{
    public static class ApplicationDbInitializer
    {
        private const string adminUser = "raufe.m@gmail.com";
        private const string defaultPassword = "101112";
        private const string productListCacheKey = "productList";

        public static void Seed(ApplicationDbContext context, IMemoryCache cache, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Roles.ROLE_API).Result)
            {
                var adminResult = roleManager.CreateAsync(new IdentityRole(Roles.ROLE_ADMIN)).Result;

                var defaultResult = roleManager.CreateAsync(new IdentityRole(Roles.ROLE_API)).Result;

                var companyResult = roleManager.CreateAsync(new IdentityRole(Roles.ROLE_COMPANY)).Result;

                if (!defaultResult.Succeeded || !companyResult.Succeeded || !adminResult.Succeeded)
                {
                    throw new Exception($"Failed to create roles.");
                }
            }

            var defaultUserExists = userManager.FindByEmailAsync(adminUser).Result;

            if (defaultUserExists is null)
            {
                ApplicationUser usuario = new()
                {
                    Email = adminUser,
                    UserName = adminUser
                };

                var newUserResponse = userManager.CreateAsync(usuario, defaultPassword).Result;

                if (newUserResponse.Succeeded)
                {
                    var addRoleSuccess = userManager.AddToRoleAsync(usuario, Roles.ROLE_ADMIN).Result;
                }
                else
                {
                    throw new Exception(
                        $"Failed to create user.");
                }
            }

            var productlist = context.Product.Include(x => x.Company).ToList();

            var options = new MemoryCacheEntryOptions().SetSize(productlist.Count());

            cache.Set<List<Product>>(productListCacheKey, productlist, options);
        }
    }
}
