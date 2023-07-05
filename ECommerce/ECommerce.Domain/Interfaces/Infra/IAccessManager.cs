using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Infra
{
    public interface IAccessManager
    {
        Task<bool> CreateUser(User user);
        AcessToken GenerateToken(User user);
        Task<ApplicationUser> UserExists(string userEmail);
        Task<bool> ValidateCredentials(User user);
    }
}
