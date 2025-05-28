using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sprint03.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // continua o pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado."); // registra no log
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var resposta = new
            {
                StatusCode = context.Response.StatusCode,
                Mensagem = "Erro interno no servidor. Por favor, tente novamente mais tarde.",
                Detalhes = exception.Message
            };

            var json = JsonSerializer.Serialize(resposta);
            return context.Response.WriteAsync(json);
        }
    }
}
