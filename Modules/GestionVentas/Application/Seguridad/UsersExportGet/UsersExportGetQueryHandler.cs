using Bigstick.BuildingBlocks.Reports;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet;
using Ferreteria.Modules.GestionVentas.Domain.Enums;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersExportGet
{
    public class UsersExportGetQueryHandler : IQueryHandler<UsersExportGetQuery, byte[]>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly IReporte _report;

        public UsersExportGetQueryHandler(ISeguridadRepository seguridadRepository, IReporte report)
        {
            _seguridadRepository = seguridadRepository;
            _report = report;
        }

        public async Task<byte[]> Handle(UsersExportGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _seguridadRepository.UsersGet(request.Nombre, request.Rol, request.Estado, request.StartAt, request.MaxResult);

            foreach (var usuario in result.Item1)
            {
                usuario.RolNombre = Enum.Parse<RolUsuario>(usuario.Rol).ToDescripcion();
                usuario.EstadoNombre = ((EstadoRegistro)usuario.EstadoRegistro).ToDescripcion();
            }

            var response = result.Item1.Select(x => new UsersExportGetDTO
            {
                Usuario = x.Usuario,
                Nombre = x.Nombre,
                ApellidoPaterno = x.ApellidoPaterno,
                ApellidoMaterno = x.ApellidoMaterno,
                Correo = x.Correo,
                Estado = x.EstadoNombre,
                Rol = x.RolNombre,
                Telefono = x.Telefono,
            });

            return await _report.GenerarReporte<UsersExportGetDTO>(response, "Usuarios");
        }
    }
}
