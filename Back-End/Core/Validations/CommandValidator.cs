using Dietbox.ECommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Validations
{
    public class CommandValidator : ICommandValidator
    {

        private readonly IServiceProvider _services;

        public CommandValidator(IServiceProvider services)
        {
            _services = services;
        }

        async Task<(bool isValid, List<string> messages)> ICommandValidator.Validate<TValidator, TCommand>(TCommand command)
        {
            IValidator<TCommand>? validator = _services.GetService(typeof(TValidator)) as IValidator<TCommand>;
            if (validator is null) { throw new Exception("Validator não injetado nas dependências da aplicação."); }
            (bool isValid, List<string> messages) = await validator.Validate(command);
            return (isValid, messages);
        }
    }
}
