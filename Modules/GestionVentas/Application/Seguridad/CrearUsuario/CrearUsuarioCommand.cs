using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.CrearUsuario
{
    public class CrearUsuarioCommand : CommandBase<RequestResult>
    {
        public CrearUsuarioCommand(string sociedad, string usuario, string correo, string nombre, string apellidoPaterno, string apellidoMaterno, string telefono, string contrasenia, string rol, string confirmarContrasenia)
        {
            Sociedad = sociedad;
            Usuario = usuario;
            Correo = correo;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Telefono = telefono;
            Contrasenia = contrasenia;
            Rol = rol;
            ConfirmarContrasenia = confirmarContrasenia;
        }

        public string Sociedad { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; }
        public string ConfirmarContrasenia { get; set; }
    }
}
