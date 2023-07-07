using Dietbox.ECommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Exceptions
{
    public class InvalidParameterException : Exception, IApplicationException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string[] Messages { get; }

        /// <summary>
        /// Excessão de parâmetro inválido, retorna um HttpStatusCode 400.
        /// </summary>
        /// <param name="messages">Mensagens.</param>
        public InvalidParameterException(string[] messages)
        {
            Messages = messages;
        }

        /// <summary>
        /// Excessão de parâmetro inválido, retorna um HttpStatusCode 400.
        /// </summary>
        /// <param name="messages">Array de mensagens.</param>
        /// <param name="title">Título.</param>
        public InvalidParameterException(string message)
        {
            Messages = new string[] { message };
        }

    }
}
