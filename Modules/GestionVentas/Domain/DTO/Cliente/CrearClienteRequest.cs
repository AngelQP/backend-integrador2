using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente
{
    public class CrearClienteRequest
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool EsEmpresa { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
