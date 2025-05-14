using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.CrearUsuario;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Cliente.CrearCliente
{
    public class CrearClienteComandHandler : ICommandHandler<CrearClienteComand, RequestResult>
    {
        private readonly IClienteRepository _ClienteRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public CrearClienteComandHandler(IClienteRepository ClienteRepository, ICommonService commonService, IUserContext userContext)
        {
            _ClienteRepository = ClienteRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(CrearClienteComand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId.Value;

            var result = await _ClienteRepository.CrearCliente(new CrearClienteRequest
            {
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                Dni = request.Dni,
                Ruc = request.Ruc,
                Direccion = request.Direccion,
                Correo = request.Correo,
                Telefono = request.Telefono,
                EsEmpresa = request.EsEmpresa,
                FechaRegistro = DateTime.UtcNow
            });

            return RequestResult.Success();
        }
    }
}
