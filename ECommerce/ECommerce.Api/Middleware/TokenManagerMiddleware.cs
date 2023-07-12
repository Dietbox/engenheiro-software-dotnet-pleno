using ECommerce.Domain.Interfaces.Infra;
using System.Net;

namespace ECommerce.Api.Middleware
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly IAccessManager _tokenManager;

        public TokenManagerMiddleware(IAccessManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var user = context.User.Identity.Name;

            if (_tokenManager.IsCurrentActiveToken(user))
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
