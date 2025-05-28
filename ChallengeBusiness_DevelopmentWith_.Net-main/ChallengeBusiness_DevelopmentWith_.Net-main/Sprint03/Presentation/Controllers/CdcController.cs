using Microsoft.AspNetCore.Mvc;
using Sprint03.Services;
using Sprint03.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint03.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CdcController : ControllerBase
    {
        private readonly ICdcApiService _cdcApiService;

        public CdcController(ICdcApiService cdcApiService)
        {
            _cdcApiService = cdcApiService;
        }

        [HttpGet("dados-dentais")]
        [SwaggerOperation(Summary = "Obtém os 10 primeiros registros de saúde bucal da CDC.")]
        [SwaggerResponse(200, "Registros retornados com sucesso")]
        public async Task<ActionResult<List<CdcDentalDto>>> Get()
        {
            var dados = await _cdcApiService.ObterDadosDentaisAsync();
            return Ok(dados.Take(10).ToList()); // <- Atualização aqui
        }

        [HttpGet("comparar")]
        [SwaggerOperation(Summary = "Filtra registros por ano ou faixa etária.")]
        [SwaggerResponse(200, "Registros filtrados com sucesso")]
        public async Task<ActionResult<List<CdcDentalDto>>> Comparar(
            [FromQuery, SwaggerParameter("Ano da pesquisa (ex: 2020)", Required = false)] string? year,
            [FromQuery, SwaggerParameter("Faixa etária (ex: Adult)", Required = false)] string? category)
        {
            var dados = await _cdcApiService.ObterDadosDentaisAsync();

            var filtrado = dados
                .Where(d =>
                    (year == null || d.Year == year) &&
                    (category == null || d.Category == category))
                .ToList();

            return Ok(filtrado);
        }

        [HttpGet("alertas")]
        [SwaggerOperation(Summary = "Gera alertas automáticos com base nos valores dos dados CDC.")]
        [SwaggerResponse(200, "Alertas gerados com sucesso")]
        public async Task<ActionResult<List<object>>> GerarAlertas()
        {
            var dados = await _cdcApiService.ObterDadosDentaisAsync();

            var alertas = dados.Take(20).Select(d => new
            {
                d.Year,
                d.Indicator,
                d.DataValue,
                Alerta = double.TryParse(d.DataValue, out var val) && val > 50
                    ? "⚠️ Alta prevalência de problema dentário"
                    : "✅ Dentro do padrão esperado"
            });

            return Ok(alertas);
        }

        [HttpGet("recomendacao-dentista")]
        [SwaggerOperation(Summary = "Recomenda o tempo ideal de ida ao dentista com base na idade.")]
        [SwaggerResponse(200, "Recomendação gerada com sucesso")]
        public ActionResult<object> RecomendacaoPorIdade(
            [FromQuery, SwaggerParameter("Idade do paciente (ex: 35)", Required = true)] int idade)
        {
            string recomendacao;

            if (idade <= 5)
                recomendacao = "👶 A cada 6 meses (crianças pequenas)";
            else if (idade <= 17)
                recomendacao = "🧒 A cada 12 meses (crianças e adolescentes)";
            else if (idade <= 59)
                recomendacao = "🧑 A cada 12 meses (adultos)";
            else
                recomendacao = "👴 A cada 6 meses (idosos)";

            return Ok(new
            {
                Idade = idade,
                Recomendacao = recomendacao
            });
        }
    }
}
