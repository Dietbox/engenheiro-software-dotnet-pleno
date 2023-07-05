using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
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
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccessManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> CreateUser(UserLogin user, string? role = null)
        {
            ApplicationUser newUser = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.Email,
                EmailConfirmed = true,
            };

            var usuarioExistente = await GetUser(newUser.Email);

            if (usuarioExistente is null)
            {
                var newUserResponse = await _userManager.CreateAsync(newUser, user.Password);

                if (newUserResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, string.IsNullOrEmpty(role) ? Roles.ROLE_API : role);
                }

                return newUser;
            }

            return null;
        }
        public async Task<ApplicationUser> GetUser(string userEmail)
        {
            ApplicationUser user = new();

            if (userEmail != null && !string.IsNullOrWhiteSpace(userEmail))
                user = await _userManager.FindByEmailAsync(userEmail);

            return user;
        }
        public async Task<ApplicationUser> ValidateCredentials(UserLogin user)
        {
            ApplicationUser identityUser = await GetUser(user.Email);

            if (identityUser is not null)
            {
                var result = await _signInManager
                    .CheckPasswordSignInAsync(identityUser, user.Password, false);

                if (result.Succeeded)
                {
                    return identityUser;
                }
            }

            return identityUser;
        }
        public async Task<AcessToken> GenerateToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>() 
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
            };

            foreach (var role in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Login"), claims);

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
        public void DeactivateCurrentAsync()
        {
            DeactivateToken(GetCurrentTokenFromHeader());
        }
        public void DeactivateToken(string token)
        {
            var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(
                            TimeSpan.FromSeconds(_tokenConfigurations.Seconds));

            _cache.Set(token, token, options);
        }
        private string GetCurrentTokenFromHeader()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
        public bool IsCurrentActiveToken()
        {
            return IsActive(GetCurrentTokenFromHeader());
        }
        public bool IsActive(string token)
        {
            if (_cache.TryGetValue(token, out string tokenStored))
            {
                tokenStored = _cache.Get<string>(token);

                if (tokenStored is null)
                    return true;
                
                return !tokenStored.Equals(token);
            }

            return true;
        }
    }
}
