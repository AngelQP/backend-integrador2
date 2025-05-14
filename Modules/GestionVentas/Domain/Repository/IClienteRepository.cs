using Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Repository
{
    public interface IClienteRepository
    {
        Task<int> CrearCliente(CrearClienteRequest request);
    }
}
