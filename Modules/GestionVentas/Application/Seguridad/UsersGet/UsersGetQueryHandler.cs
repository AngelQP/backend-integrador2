using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
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

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet
{
    public class UsersGetQueryHandler : IQueryHandler<UsersGetQuery, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IMailService _mailService;
        private readonly IUserContext _userContext;

        public UsersGetQueryHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IMailService mailService, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _mailService = mailService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(UsersGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _seguridadRepository.UsersGet(request.Nombre, request.StartAt, request.MaxResult);

            foreach (var usuario in result.Item1)
            {
                usuario.RolNombre = Enum.Parse<RolUsuario>(usuario.Rol).ToDescripcion();
                usuario.EstadoNombre = ((EstadoRegistro)usuario.EstadoRegistro).ToDescripcion();
            }

            var response = new UsersGetDTO(result.Item1, request.StartAt, request.MaxResult, result.Item2);

            return RequestResult.Success(response);
        }
    }
}
