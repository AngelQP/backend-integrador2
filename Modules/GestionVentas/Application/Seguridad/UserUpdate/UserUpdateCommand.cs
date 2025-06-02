using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UserUpdate
{ 
    public class UserUpdateCommand : CommandBase<RequestResult>
    {
        public UserUpdateCommand(int idUsuario, string correo, string nombre, string apellidoPaterno, string apellidoMaterno, string telefono, string rol, bool actualizarContrasenia, string contrasenia, string confirmarContrasenia)
        {
            IdUsuario = idUsuario;
            Correo = correo;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Telefono = telefono;
            Rol = rol;
            ActualizarContrasenia = actualizarContrasenia;
            Contrasenia = contrasenia;
            ConfirmarContrasenia = confirmarContrasenia;
        }

        //public string Sociedad { get; set; }
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
        public bool ActualizarContrasenia { get; set; }
        public string Contrasenia { get; set; }
        public string ConfirmarContrasenia { get; set; }
    }
}
