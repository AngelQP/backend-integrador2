using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor
{
    public class CrearProveedorRequest
    {
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contacto { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
