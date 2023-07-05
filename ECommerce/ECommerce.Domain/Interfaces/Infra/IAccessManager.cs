using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Infra
{
    public interface IAccessManager
    {
        Task<bool> CreateUser(User user);
        void DeactivateCurrentAsync();
        void DeactivateToken(string token);
        AcessToken GenerateToken(User user);
        bool IsActive(string token);
        bool IsCurrentActiveToken();
        Task<ApplicationUser> UserExists(string userEmail);
        Task<bool> ValidateCredentials(User user);
    }
}
