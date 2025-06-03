using Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.GetProveedor
{
    public class ProveedorGetDTO
    {
        public ProveedorGetDTO(IEnumerable<ProveedorDTO> proveedor, int startAt, int maxResult, int total)
        {
            Proveedor = proveedor;
            StartAt = startAt;
            MaxResult = maxResult;
            Total = total;
        }
        
        public IEnumerable<ProveedorDTO> Proveedor { get; }
        public int StartAt { get; }
        public int MaxResult { get; }
        public int Total { get; }
    }
}
