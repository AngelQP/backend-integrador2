using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Cliente.CrearCliente;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.CrearProveedor
{
    public class CrearProveedorCommandHandler
    {
        private readonly IProveedorRepository _ProveedorRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public CrearProveedorCommandHandler(IProveedorRepository ProveedorRepository, ICommonService commonService, IUserContext userContext)
        {
            _ProveedorRepository = ProveedorRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(CrearProveedorCommand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId.Value;

            var result = await _ProveedorRepository.CrearProveedor(new CrearProveedorRequest
            {
                Nombre = request.Nombre,
                Ruc = request.Ruc,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Correo = request.Correo,
                Contacto = request.Contacto,
                Estado = request.Estado,
                Fecha_Registro = DateTime.UtcNow
            });

            return RequestResult.Success();
        }
    }
}
