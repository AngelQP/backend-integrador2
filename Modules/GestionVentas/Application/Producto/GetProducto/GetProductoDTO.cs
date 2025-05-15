using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto
{
    public class GetProductoDTO
    {
        public GetProductoDTO(IEnumerable<ProductoDTO> producto, int startAt, int maxResult, int total)
        {
            Producto = producto;
            StartAt = startAt;
            MaxResult = maxResult;
            Total = total;
        }

        public IEnumerable<ProductoDTO> Producto { get; }
        public int StartAt { get; }
        public int MaxResult { get; }
        public int Total { get; }
    }
}