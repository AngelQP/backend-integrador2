using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.CrearProveedor
{
    public class CrearProveedorCommand : CommandBase<RequestResult>
    {
        public CrearProveedorCommand(string nombre, string ruc, string direccion, string telefono , string correo, string contacto, bool estado, DateTime fecha_registro)
        { 
            Nombre = nombre;
            Ruc = ruc;
            Direccion = direccion;
            Telefono = telefono;
            Correo = correo;
            Contacto = contacto;
            Estado = estado;
            Fecha_Registro = fecha_registro;
        }

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
