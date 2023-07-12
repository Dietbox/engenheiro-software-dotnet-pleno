using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserApp(UserAppRegister userApp, string? role = null);
    }
}
