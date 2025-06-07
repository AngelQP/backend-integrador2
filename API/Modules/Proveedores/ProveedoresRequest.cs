using System;

namespace Ferreteria.GestionVentas.API.Modules.Proveedores
{
	public class ProveedoresRequest
	{
		public string Nombre { get; set; }
		public string Ruc { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string Contacto { get; set; }
		public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
