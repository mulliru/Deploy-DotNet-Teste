using System.Net.Http;
using System.Text.Json;
using Sprint03.Dtos;

namespace Sprint03.Services
{
    public class CdcApiService : ICdcApiService
    {
        private readonly HttpClient _httpClient;

        public CdcApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CdcDentalDto>> ObterDadosDentaisAsync()
        {
            var response = await _httpClient.GetAsync("https://data.cdc.gov/resource/jz6n-v26y.json");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var dados = JsonSerializer.Deserialize<List<CdcDentalDto>>(json);

            return dados!;
        }
    }
}
