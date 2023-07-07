using Dietbox.ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietbox.ECommerce.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("public")]
    [ApiController]
    public class PublicController : BaseController
    {
        private readonly ISettings _settings;
        public PublicController(ISettings settings)
        {
            _settings = settings;
        }


        [HttpGet("recaptcha")]
        public async Task<IActionResult> CheckRecaptchaEnabled()
        {
            bool recaptchaEnabled = _settings.Recaptcha.Enabled;
            return Ok(new { Enabled = recaptchaEnabled });
        }
    }
}
