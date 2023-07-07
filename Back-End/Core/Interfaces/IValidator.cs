using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces
{
    public interface IValidator<TCommand> where TCommand : class
    {
        Task<(bool isValid, List<string> messages)> Validate(TCommand command);
    }
}
