using System;

namespace Ferreteria.GestionVentas.API.Modules.Clientes
{
    public class ClienteRequest
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
