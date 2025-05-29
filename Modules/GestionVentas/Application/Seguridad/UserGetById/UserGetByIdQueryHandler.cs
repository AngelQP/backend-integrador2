using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Domain.Enums;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UserGetById
{
    public class UserGetByIdQueryHandler : IQueryHandler<UserGetByIdQuery, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IMailService _mailService;
        private readonly IUserContext _userContext;

        public UserGetByIdQueryHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IMailService mailService, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _mailService = mailService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _seguridadRepository.UserGetById(request.IdUsuario);

            if (result == null)
                return RequestResult.WithError("El usuario no se encontró.");

            result.RolNombre = Enum.Parse<RolUsuario>(result.Rol).ToDescripcion();
            result.EstadoNombre = ((EstadoRegistro)result.EstadoRegistro).ToDescripcion();

            return RequestResult.Success(result);
        }
    }
}
