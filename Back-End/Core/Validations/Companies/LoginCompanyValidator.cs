using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Core.Utils;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Validations.Companies
{
    public class LoginCompanyValidator : IValidator<LoginCompanyCommand>
    {
        private readonly IRepository<Company> _repository;
        private readonly List<string> _messages;
        public LoginCompanyValidator(
            IRepository<Company> repository
        )
        {
            _repository = repository;
            _messages = new();
        }

        public async Task<(bool isValid, List<string> messages)> Validate(LoginCompanyCommand command)
        {
            if (command is null)
                return (false, new() { "Parâmetro inválido." });

            if (string.IsNullOrEmpty(command.Email))
                _messages.Add("E-mail não informado.");

            command.Email = command.Email.Trim();
            command.Email = command.Email.ToLower();

            if (string.IsNullOrEmpty(command.Password))
                _messages.Add("Senha não informada.");

            command.Password = command.Password.ToHash256();

            if (_messages.Any()) { return (false, _messages); }

            bool existsCompany = await _repository.Get(_ => _.Email == command.Email).AnyAsync();
            if (existsCompany is false)
                return (false, new() { "Nenhuma empresa foi encontrada utilizando o e-mail informado." });

            string? password = await _repository
                .Get(_ => _.Email == command.Email)
                .Select(_ => _.Password)
                .FirstOrDefaultAsync();

            if (password != command.Password)
                return (false, new() { "Senha incorreta." });

            return (true, new());

        }
    }
}
