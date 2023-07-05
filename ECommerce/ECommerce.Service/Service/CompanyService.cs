using ECommerce.Domain.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Interfaces.Repository;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;

namespace ECommerce.Service.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IAccessManager _accessManager;
        private readonly IGenericRepository<Company> _companyRepository;

        public CompanyService(IAccessManager accessManager, IGenericRepository<Company> companyRepository)
        {
            _accessManager = accessManager;
            _companyRepository = companyRepository;
        }

        public async Task<bool> CreateUserApp(CompanyAppRegister companyUser)
        {
            var userCreated = await _accessManager.CreateUser(companyUser, Roles.ROLE_COMPANY);

            if (userCreated is not null)
            {
                var newCompany = new Company(companyUser.TradingName, companyUser.TaxId, userCreated);

                await _companyRepository.AddAsync(newCompany);

                await _companyRepository.SaveAsync();

                return true;
            }

            return false;
        }
    }
}
