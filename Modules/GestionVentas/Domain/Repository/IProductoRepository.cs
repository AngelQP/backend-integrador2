using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Repository
{
    public interface IProductoRepository
    {
        Task<int> CrearProducto(CrearProductoRequest request);
    }
}
