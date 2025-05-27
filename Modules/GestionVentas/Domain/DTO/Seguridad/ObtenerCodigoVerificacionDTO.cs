using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad
{
    public class ObtenerCodigoVerificacionDTO
    {
        public int IdCodigoVerificacion { get; set; }
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Codigo { get; set; }
        public DateTime Expiracion { get; set; }
        public bool Usado { get; set; }
    }
}
