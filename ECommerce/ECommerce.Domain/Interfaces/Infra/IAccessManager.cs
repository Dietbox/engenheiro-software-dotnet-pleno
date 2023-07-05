using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Models;
using System.Data;

namespace ECommerce.Domain.Interfaces.Infra
{
    public interface IAccessManager
    {
        Task<ApplicationUser> CreateUser(UserLogin user);
        void DeactivateCurrentAsync();
        void DeactivateToken(string token);
        AcessToken GenerateToken(UserLogin user);
        bool IsActive(string token);
        bool IsCurrentActiveToken();
        Task<ApplicationUser> UserExists(string userEmail);
        Task<bool> ValidateCredentials(UserLogin user);
    }
}
