using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Commands.Customers;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Core.Utils;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Users;
using Dietbox.ECommerce.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Validations.Customers
{
    public class LoginCustomerValidator : IValidator<LoginCustomerCommand>
    {

        private readonly IRepository<Customer> _repository;
        private readonly List<string> _messages;
        public LoginCustomerValidator(IRepository<Customer> repository)
        {
            _repository = repository;
            _messages = new();
        }


        public async Task<(bool isValid, List<string> messages)> Validate(LoginCustomerCommand command)
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
                return (false, new() { "Nenhum cliente foi encontrado utilizando o e-mail informado." });

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
