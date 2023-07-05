using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Infra
{
    public interface IAccessManager
    {
        Task<ApplicationUser> CreateUser(UserLogin user, string? role);
        void DeactivateCurrentAsync();
        void DeactivateToken(string token);
        Task<AcessToken> GenerateToken(ApplicationUser user);
        bool IsActive(string token);
        bool IsCurrentActiveToken();
        Task<ApplicationUser> GetUser(string userEmail);
        Task<ApplicationUser> ValidateCredentials(UserLogin user);
    }
}
