using Sprint03.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint03.Services
{
    public interface ICdcApiService
    {
        Task<List<CdcDentalDto>> ObterDadosDentaisAsync();
    }
}
