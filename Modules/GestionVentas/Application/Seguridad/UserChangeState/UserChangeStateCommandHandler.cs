using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Domain.Enums;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UserChangeState
{
    public class UserChangeStateCommandHandler : ICommandHandler<UserChangeStateCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly IUserContext _userContext;

        public UserChangeStateCommandHandler(ISeguridadRepository seguridadRepository, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(UserChangeStateCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(EstadoRegistro), request.Estado))
                return RequestResult.WithError("Estado inválido. Solo se permiten los valores 0 (Inactivo) y 1 (Activo).");

            var usuario = _userContext.UserId.UserName;

            var result = await _seguridadRepository.CambiarEstadoUsuario(request.IdUsuario, request.Estado, usuario);

            return new RequestResult();
        }
    }
}
