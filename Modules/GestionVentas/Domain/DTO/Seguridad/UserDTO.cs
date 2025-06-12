using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad
{
    public class UserDTO
    {
        public int IdUsuario { get; set; }
        public string Sociedad { get; set; }
        public string SociedadNombre { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
        public string RolNombre { get; set; }
        public int EstadoRegistro { get; set; }
        public string EstadoNombre { get; set; }
    }
}
