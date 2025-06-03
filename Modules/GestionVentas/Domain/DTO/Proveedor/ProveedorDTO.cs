using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor
{
	public class ProveedorDTO
	{
		public int IdProveedor { get; set; }
		public string Nombre { get; set; }
		public string RazonSocial { get; set; }
		public string Ruc { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string Contacto { get; set; }
		public int EstadoRegistro { get; set; }
		public string EstadoNombre { get; set; }
	}
}

