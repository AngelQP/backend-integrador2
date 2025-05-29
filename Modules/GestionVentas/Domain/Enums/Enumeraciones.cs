using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Enums
{
    public enum RolUsuario
    {
        ADMIN_GENERAL,
        ADMIN_TIENDA,
        CAJERO
    }

    public static class RolUsuarioExtensions
    {
        public static string ToDescripcion(this RolUsuario rol)
        {
            return rol switch
            {
                RolUsuario.ADMIN_GENERAL => "Administrador General",
                RolUsuario.ADMIN_TIENDA => "Administrador Tienda",
                RolUsuario.CAJERO => "Cajero",
                _ => "Desconocido"
            };
        }
    }

    public enum EstadoRegistro
    {
        Inactivo = 0,
        Activo = 1
    }

    public static class EstadoRegistroExtensions
    {
        public static string ToDescripcion(this EstadoRegistro estado)
        {
            return estado switch
            {
                EstadoRegistro.Activo => "Activo",
                EstadoRegistro.Inactivo => "Inactivo",
                _ => "Desconocido"
            };
        }
    }
}
