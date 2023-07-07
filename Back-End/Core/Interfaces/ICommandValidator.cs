using Dietbox.ECommerce.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces
{
    public interface ICommandValidator
    {
        Task<(bool isValid, List<string> messages)> Validate<TValidator, TCommand>(TCommand command) where TValidator : IValidator<TCommand> where TCommand : Command;
    }
}
