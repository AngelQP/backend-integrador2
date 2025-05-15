using Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Repository
{
    public interface IProveedorRepository
    {
        Task<int> CrearProveedor(CrearProveedorRequest request);
    }
}
