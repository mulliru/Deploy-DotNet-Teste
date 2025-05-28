using Microsoft.EntityFrameworkCore;
using Sprint03.Infrastructure.Data;
using Sprint03.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Oracle.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Sprint03.Services;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Configuração do banco de dados Oracle.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Repositórios e serviços
builder.Services.AddScoped<NomeUsuarioRepository>();
builder.Services.AddHttpClient<ICdcApiService, CdcApiService>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sprint03 API",
        Version = "v1",
        Description = "API para gerenciamento de usuários."
    });

    options.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de exceções e Rate Limiting
app.UseMiddleware<Sprint03.Middleware.ExceptionMiddleware>();
app.UseIpRateLimiting(); // << Adicionado aqui

app.UseHttpsRedirection();
app.UseMiddleware<Sprint03.Middleware.IdempotencyMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
