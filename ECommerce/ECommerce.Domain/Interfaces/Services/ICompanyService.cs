using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<bool> CreateUserApp(CompanyAppRegister companyUser);
    }
}
