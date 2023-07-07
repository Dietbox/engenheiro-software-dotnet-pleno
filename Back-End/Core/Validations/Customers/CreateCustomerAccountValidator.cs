using Dietbox.ECommerce.Core.Commands.Users;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Core.Services;
using Dietbox.ECommerce.ORM.Entities.Users;
using Dietbox.ECommerce.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Validations.Customers
{
    public class CreateCustomerAccountValidator : IValidator<CreateCustomerAccountCommand>
    {

        private readonly IRepository<Customer> _repository;
        private readonly ISettings _settings;
        private readonly GoogleRecaptcha _googleRecaptcha;
        private readonly List<string> _messages;

        private const int _NAME_MAX_LENGTH = 100;
        private const int _EMAIL_MAX_LENGTH = 80;
        private const int _PASSWORD_MIN_LENGTH = 4;
        private const int _PASSWORD_MAX_LENGTH = 30;

        public CreateCustomerAccountValidator(
            IRepository<Customer> repository,
            ISettings settings,
            GoogleRecaptcha googleRecaptcha
        )
        {
            _repository = repository;
            _settings = settings;
            _googleRecaptcha = googleRecaptcha;
            _messages = new();
        }

        public async Task<(bool isValid, List<string> messages)> Validate(CreateCustomerAccountCommand command)
        {
            if (command is null)
                return (false, new() { "Parâmetro inválido." });


            #region Google Recaptcha

            if (_settings.Recaptcha.Enabled)
            {

                if (string.IsNullOrEmpty(command.Recaptcha))
                    return (false, new() { "Recaptcha não validado." });

                bool recapchaIsValid = await _googleRecaptcha.Validate(command.Recaptcha);
                if (recapchaIsValid is false)
                    return (false, new() { "Recaptcha expirado." });

            }

            #endregion


            #region Nome do Cliente

            if (string.IsNullOrEmpty(command.Name))
            {
                _messages.Add("Nome não informado.");
            }
            else
            {
                command.Name = command.Name.Trim();

                if (command.Name.Length > _NAME_MAX_LENGTH)
                    _messages.Add(string.Format("O  nome não pode exceder o limite de {0} caracteres.", _NAME_MAX_LENGTH));

                if (command.Name.All(char.IsDigit))
                    _messages.Add("O nome não pode conter somente números.");
            }

            #endregion


            #region E-mail do Cliente

            if (string.IsNullOrEmpty(command.Email))
            {
                _messages.Add("E-mail não informado.");
            }
            else
            {
                command.Email = command.Email.Trim();
                command.Email = command.Email.ToLower();

                if (command.Email.Length > _EMAIL_MAX_LENGTH)
                    _messages.Add(string.Format("O e-mail não pode exceder o limite de {0} caracteres.", _EMAIL_MAX_LENGTH));


                bool isValidEmail = MailAddress.TryCreate(command.Email, out var _);
                if (isValidEmail is false)
                    _messages.Add("E-mail inválido.");

            }

            #endregion


            #region Senha

            if (string.IsNullOrEmpty(command.Password))
            {
                _messages.Add("Senha não informada.");
            }
            else
            {

                if (command.Password.Length < _PASSWORD_MIN_LENGTH)
                    _messages.Add(string.Format("A senha precisa conter pelo menos {0} caracteres.", _PASSWORD_MIN_LENGTH));

                if (command.Password.Length > _PASSWORD_MAX_LENGTH)
                    _messages.Add(string.Format("A senha não pode exceder o limite de {0} caracteres.", _PASSWORD_MAX_LENGTH));
            }

            #endregion

            // Retornar caso houver problemas na validação dos campos do comando:
            if (_messages.Any()) return (false, _messages);

            // Validar se o e-mail já está cadastrado:
            bool existsEmail = await _repository.Get(_ => _.Email == command.Email).AnyAsync();
            if (existsEmail)
                return (false, new() { "O e-mail informado já está em uso." });

            return (true, new());

        }
    }
}
