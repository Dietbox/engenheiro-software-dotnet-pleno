using Dietbox.ECommerce.Core.DTO.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Dietbox.ECommerce.WebAPI.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            ResponseAPI result = new(false, value);
            return base.Ok(result);
        }

        protected OkObjectResult Ok([ActionResultObjectValue] object? value, string message)
        {
            ResponseAPI result = new(false, message, value);
            return base.Ok(result);
        }

        protected OkObjectResult Ok([ActionResultObjectValue] object? value, string[] messages)
        {
            ResponseAPI result = new ResponseAPI(false, messages, value);
            return base.Ok(result);
        }
    }

}
