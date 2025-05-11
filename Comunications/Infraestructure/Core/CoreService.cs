using Bigstick.BuildingBlocks.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.Core;
using Ferreteria.Comunications.Application.Core.Core.DTO;

namespace Ferreteria.Comunications.Infrastructure.Core
{
    public class CoreService : ICoreService
    {
        private readonly IHttpClientService _httpClient;
        private string _urlBase { get; }

        public CoreService(IHttpClientService httpClient, string urlBase)
        {
            _httpClient = httpClient;
            _urlBase = urlBase;
        }

        public async Task<IEnumerable<TipoCambioGetByFechaDTO>> TipoCambio(DateTime? fecha)
        {
            return await _httpClient
                .GetOkAsync<IEnumerable<TipoCambioGetByFechaDTO>>($"{_urlBase}TipoCambio?fecha={fecha.Value.Year}-{fecha.Value.Month}-{fecha.Value.Day}");
        }
    }
}
