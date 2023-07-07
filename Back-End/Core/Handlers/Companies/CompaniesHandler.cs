using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.DTO.API;
using Dietbox.ECommerce.Core.DTO.Companies;
using Dietbox.ECommerce.Core.Exceptions;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Core.Interfaces.Companies;
using Dietbox.ECommerce.Core.Mappers.Companies;
using Dietbox.ECommerce.Core.Services;
using Dietbox.ECommerce.Core.Validations.Companies;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Interfaces;
using Dietbox.ECommerce.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Handlers.Companies
{
    public class CompaniesHandler : ICompaniesHandler
    {

        private readonly IRepository<Company> _repository;
        private readonly ICommandValidator _validator;
        private readonly CompanyMapperConfiguration _mapper;
        private readonly JsonWebToken _jsonWebToken;

        public CompaniesHandler(
            IRepository<Company> repository,
            ICommandValidator validator,
            CompanyMapperConfiguration mapper,
            JsonWebToken jsonWebToken
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _jsonWebToken = jsonWebToken;
        }

        public async Task CreateAccount(CreateCompanyAccountCommand command)
        {
            (bool isValid, List<string> messages) = await _validator.Validate<CreateCompanyAccountValidator, CreateCompanyAccountCommand>(command);
            if (isValid is false)
                throw new InvalidParameterException(messages.ToArray());

            Company entity = _mapper.CreateMapper().Map<Company>(command);
            await _repository.Insert(entity);
        }

        public async Task<AuthorizationDTO> Login(LoginCompanyCommand command)
        {
            (bool isValid, List<string> messages) = await _validator.Validate<LoginCompanyValidator, LoginCompanyCommand>(command);
            if (isValid is false)
                throw new InvalidParameterException(messages.ToArray());

            Company? entity = await _repository
                .Get(_ => _.Email == command.Email && _.Password == command.Password)
                .FirstOrDefaultAsync();

            (string token, DateTime? expiration) = _jsonWebToken.GenerateToken(entity);
            AuthorizationDTO authorization = new(token, (DateTime)expiration, entity.Name, TenantType.Company);
            return authorization;
        }

    }
}
