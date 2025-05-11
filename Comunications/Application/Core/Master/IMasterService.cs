using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.Master.DTO;

namespace Ferreteria.Comunications.Application.Core.Master
{
    public interface IMasterService
    {
        Task<IEnumerable<SucursalDTO>> SucursalGet(string codigoSociedad);
        Task<IEnumerable<DepositoDTO>> DepositoGet(string codigoSucursal);
        Task<IEnumerable<LineaDTO>> LineasGet(string codigoSociedad, string codigoPais);
        Task<IEnumerable<NotificacionDTO>> NotificacionGet(string codigoAplicacion, string codigoLinea, string codigo);
        Task<IEnumerable<CatalagoGetByDTO>> CatalogoGetBy(string codigoAplicacion, string codigoCatalogo, string codigoSociedad, string codigoPais);
        Task<IEnumerable<UbigeoGetByUbigeoDTO>> UbigeoGetByUbigeo(string codigoUbigeo);
        Task<IEnumerable<OperadorPortuarioGetDepositoDTO>> OperadorPortuario(string codigoSucursal);
        Task<IEnumerable<RolDTO>> RolesGet();
        Task<IEnumerable<SociedadDTO>> SociedadGet();
        Task<IEnumerable<CuentaCorrienteDTO>> CuentaCorrienteGet(string codigoSociedad);
        Task<IEnumerable<CuentaCorrienteGetByBancoDTO>> CuentaCorrienteGetByBanco(string codigoSociedad, string codigoBanco);
    }
}
