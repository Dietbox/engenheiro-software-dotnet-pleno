using Dietbox.ECommerce.Core.DTO.API;
using Dietbox.ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Dietbox.ECommerce.WebAPI.Configurations.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseAppExceptions(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(a => a.Run(async context =>
            {

                IExceptionHandlerPathFeature? exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
                Exception? error = exceptionHandler is null ? new Exception("Erro não identificado.") : exceptionHandler.Error;
                string[]? messages = null;

                // Esse código verifica se o erro é uma exceção de aplicação.
                // Se for, ele atribui o código de status da resposta para o código de status da exceção e armazena as mensagens da exceção.
                // Se não for, ele atribui o código de status da resposta para o código de status de erro interno do servidor e armazena a mensagem da exceção.

                if (error is IApplicationException)
                {
                    IApplicationException exception = (IApplicationException)error;
                    context.Response.StatusCode = (int)exception.StatusCode;
                    messages = exception.Messages;
                }
                else
                {
                    Exception exception = error;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    messages = new string[] { "Ocorreu um erro genérico no servidor.", exception.Message };
                }

                await context.Response.WriteAsJsonAsync(new ResponseAPI(true, messages));

            }));
        }
    }
}
