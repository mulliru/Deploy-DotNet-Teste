using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Sprint03.Middleware
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _next;

        public IdempotencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post)
            {
                var idempotencyKey = context.Request.Headers["Idempotency-Key"].ToString();

                if (string.IsNullOrEmpty(idempotencyKey))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Cabeçalho Idempotency-Key é obrigatório para requisições POST.");
                    return;
                }

                // Aqui você poderia adicionar verificação com banco ou cache
                // para evitar duplicidade de processamento.
            }

            await _next(context);
        }
    }
}
