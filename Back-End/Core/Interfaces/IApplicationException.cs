using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces
{
    public interface IApplicationException
    {
        public HttpStatusCode StatusCode { get; }
        public string[] Messages { get; }
    }
}
