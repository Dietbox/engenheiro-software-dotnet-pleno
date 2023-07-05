using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace ECommerce.Infra.Auth
{
    public class AccessManager : IAccessManager
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public AccessManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<bool> CreateUser(User user)
        {
            ApplicationUser usuario = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.Email,
                EmailConfirmed = true,
            };

            var usuarioExistente = await UserExists(user.Email);

            if (usuarioExistente is null)
            {
                var newUserResponse = await _userManager.CreateAsync(usuario, user.Password);

                if (newUserResponse.Succeeded)
                {
                    var roleAdded = await _userManager.AddToRoleAsync(usuario, Roles.ROLE_API);

                    return roleAdded.Succeeded;
                }

                return newUserResponse.Succeeded;
            }

            return false;
        }

        public async Task<ApplicationUser> UserExists(string userEmail)
        {
            ApplicationUser user = new();

            if (userEmail != null && !string.IsNullOrWhiteSpace(userEmail))
                user = await _userManager.FindByEmailAsync(userEmail);

            return user;
        }

        public async Task<bool> ValidateCredentials(User user)
        {
            bool validCredentials = false;

            ApplicationUser identityUser = await UserExists(user.Email);

            if (identityUser is not null)
            {
                var result = await _signInManager
                    .CheckPasswordSignInAsync(identityUser, user.Password, false);

                if (result.Succeeded)
                {
                    validCredentials = _userManager.IsInRoleAsync(identityUser, Roles.ROLE_API).Result;
                }
            }

            return validCredentials;
        }

        public AcessToken GenerateToken(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        }
                );

            DateTime createAt = DateTime.Now;
            DateTime expirationDate = createAt + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createAt,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return new AcessToken()
            {
                Authenticated = true,
                Created = createAt.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Token = token,
                Message = "OK"
            };
        }
    }
}
