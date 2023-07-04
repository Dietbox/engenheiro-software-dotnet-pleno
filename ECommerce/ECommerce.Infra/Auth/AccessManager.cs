using ECommerce.Domain.Entities;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace ECommerce.Infra.Auth
{
    public class AccessManager
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

        public async Task<bool> CriarUsuario(string email)
        {
            var defaultPassword = "101112";

            ApplicationUser usuario = new ApplicationUser()
            {
                Email = email,
                UserName = email
            };

            var usuarioExistente = await UsuarioAsync(email);

            if (usuarioExistente is null)
            {
                var newUserResponse = await _userManager.CreateAsync(usuario, defaultPassword);

                if (newUserResponse.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(usuario, Roles.ROLE_API);
                }

                return true;
            }

            return false;
        }

        public async Task<ApplicationUser> UsuarioAsync(string email)
        {
            ApplicationUser usuario = null;

            if (email != null && !string.IsNullOrWhiteSpace(email))
                usuario = await _userManager.FindByEmailAsync(email);

            return usuario;
        }

        public async Task<bool> ValidarCredenciaisAsync(User user)
        {
            bool credenciaisValidas = false;

            ApplicationUser usuarioIdentity = await UsuarioAsync(user.Email);

            if (usuarioIdentity != null)
            {
                var resultadoLogin = await _signInManager
                    .CheckPasswordSignInAsync(usuarioIdentity, user.Password, false);

                if (resultadoLogin.Succeeded)
                {
                    credenciaisValidas = _userManager.IsInRoleAsync(
                        usuarioIdentity, Roles.ROLE_API).Result;
                }
            }

            return credenciaisValidas;
        }

        public AcessToken GerarToken(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        }
                );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            var token = handler.WriteToken(securityToken);

            return new AcessToken()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                Token = token,
                Message = "OK"
            };
        }
    }
}
