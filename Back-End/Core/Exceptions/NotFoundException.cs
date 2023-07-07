using Dietbox.ECommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Exceptions
{
    public class NotFoundException : Exception, IApplicationException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string[] Messages { get; }

        /// <summary>
        /// Excessão para quando uma entidade não é encontrada, retorna um HttpStatusCode 404.
        /// </summary>
        /// <param name="messages">Array de mensagens.</param>
        public NotFoundException(string[] messages)
        {
            Messages = messages;
        }

        /// <summary>
        /// Excessão para quando uma entidade não é encontrada, retorna um HttpStatusCode 404.
        /// </summary>
        /// <param name="message">Mensagem.</param>
        public NotFoundException(string message)
        {
            Messages = new string[] { message };
        }

    }
}
