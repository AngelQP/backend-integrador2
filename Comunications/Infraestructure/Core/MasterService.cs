using Bigstick.BuildingBlocks.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.Master;
using Ferreteria.Comunications.Application.Core.Master.DTO;

namespace Ferreteria.Comunications.Infrastructure.Core
{
    public class MasterService : IMasterService
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _version;
        private string _urlBase { get; }

        public MasterService(IHttpClientService httpClientService, string urlBase, string version)
        {
            this._httpClient = httpClientService;
            this._urlBase = urlBase;
            this._version = version;
        }

        public async Task<IEnumerable<SucursalDTO>> SucursalGet(string codigoSociedad)
        {
            return await _httpClient
                       .GetOkAsync<IEnumerable<SucursalDTO>>($"{_urlBase}Sucursales?codigoSociedad={codigoSociedad}");
        }

        public async Task<IEnumerable<DepositoDTO>> DepositoGet(string codigoSucursal)
        {
            return await _httpClient
                       .GetOkAsync<IEnumerable<DepositoDTO>>($"{_urlBase}Depositos?codigoSucursal={codigoSucursal}");
        }
        public async Task<IEnumerable<LineaDTO>> LineasGet(string codigoSociedad, string codigoPais)
        {
            return await _httpClient
                .GetOkAsync<IEnumerable<LineaDTO>>($"{_urlBase}Lineas?codigoSociedad={codigoSociedad}&codigoPais={codigoPais}");
        }

        public async Task<IEnumerable<NotificacionDTO>> NotificacionGet(string codigoAplicacion, string codigoLinea, string codigo)
        {
            return await _httpClient
                .GetOkAsync<IEnumerable<NotificacionDTO>>($"{_urlBase}Notificaciones?codigoAplicacion={codigoAplicacion}&codigoLinea={codigoLinea}&codigo={codigo}");
        }

        public async Task<IEnumerable<CatalagoGetByDTO>> CatalogoGetBy(string codigoAplicacion, string codigoCatalogo, string codigoSociedad, string codigoPais)
        {
            return await _httpClient
                       .GetOkAsync<IEnumerable<CatalagoGetByDTO>>($"{_urlBase}Catalogos?codigoAplicacion={codigoAplicacion}&codigoCatalogo={codigoCatalogo}&codigoSociedad={codigoSociedad}&codigoPais={codigoPais}");
        }

        public async Task<IEnumerable<UbigeoGetByUbigeoDTO>> UbigeoGetByUbigeo(string codigoUbigeo)
        {
            return await _httpClient
                   .GetOkAsync<IEnumerable<UbigeoGetByUbigeoDTO>>($"{_urlBase}UbigeoGetByUbigeo?codigoUbigeo={codigoUbigeo}");
        }

        public async Task<IEnumerable<OperadorPortuarioGetDepositoDTO>> OperadorPortuario(string codigoSucursal)
        {
            return await _httpClient
                       .GetOkAsync<IEnumerable<OperadorPortuarioGetDepositoDTO>>($"{_urlBase}OperadorPortuario?codigoSucursal={codigoSucursal}");
        }

        public async Task<IEnumerable<RolDTO>> RolesGet()
        {
            return await _httpClient
                .GetOkAsync<IEnumerable<RolDTO>>($"{_urlBase}Roles");
        }

        public async Task<IEnumerable<SociedadDTO>> SociedadGet()
        {
            return await _httpClient.GetOkAsync<IEnumerable<SociedadDTO>>($"{_urlBase}Sociedades");
        }

        public async Task<IEnumerable<CuentaCorrienteDTO>> CuentaCorrienteGet(string codigoSociedad)
        {
            return await _httpClient
                       .GetOkAsync<IEnumerable<CuentaCorrienteDTO>>($"{_urlBase}CuentaCorriente?codigoSociedad={codigoSociedad}");
        }

        public async Task<IEnumerable<CuentaCorrienteGetByBancoDTO>> CuentaCorrienteGetByBanco(string codigoSociedad, string codigoBanco)
        {
            return await _httpClient
                       .GetOkAsync<IEnumerable<CuentaCorrienteGetByBancoDTO>>($"{_urlBase}CuentaCorrienteGetByBanco?codigoSociedad={codigoSociedad}&codigoBanco={codigoBanco}");
        }
    }
}
