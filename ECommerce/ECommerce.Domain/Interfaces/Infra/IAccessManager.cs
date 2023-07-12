using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Infra
{
    public interface IAccessManager
    {
        Task<ApplicationUser> CreateUser(UserLogin user, string? role);
        void DeactivateCurrent(string? name);
        void DeactivateToken(string token, string user);
        Task<AcessToken> GenerateToken(ApplicationUser user);
        bool IsCurrentActiveToken(string? user);
        Task<ApplicationUser> GetUser(string userEmail);
        Task<ApplicationUser> ValidateCredentials(UserLogin user);
    }
}
