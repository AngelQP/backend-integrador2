using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Producto
{
    public class UsuariosLiteDTO
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Rol { get; set; }
    }
}
