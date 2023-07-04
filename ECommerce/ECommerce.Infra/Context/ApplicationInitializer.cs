using ECommerce.Domain.Entities;
using ECommerce.Infra.Auth;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infra.Context
{
    public static class ApplicationDbInitializer
    {
        private const string defaultUser = "raufe.m@gmail.com";
        private const string defaultPassword = "101112";

        public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
           
            if (!roleManager.RoleExistsAsync(Roles.ROLE_API).Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole(Roles.ROLE_API)).Result;

                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role {Roles.ROLE_API}.");
                }
            }

            var defaultUserExists = userManager.FindByEmailAsync(defaultUser).Result;

            if (defaultUserExists is null)
            {
                ApplicationUser usuario = new()
                {
                    Email = defaultUser,
                    UserName = defaultUser
                };

                var newUserResponse = userManager.CreateAsync(usuario, defaultPassword).Result;

                if (newUserResponse.Succeeded)
                {
                    var addRoleSuccess = userManager.AddToRoleAsync(usuario, Roles.ROLE_API).Result;
                }
                else
                {
                    throw new Exception(
                        $"Failed to create user.");
                }
            }
        }
    }
}
