using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Cliente.CrearCliente
{
    public class CrearClienteComand : CommandBase<RequestResult>
    {
        public CrearClienteComand(
            string nombre,
            string apellidos,
            string dni,
            string ruc,
            string direccion,
            string telefono,
            string correo,
            bool esEmpresa,
            DateTime fechaRegistro)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Dni = dni;
            Ruc = ruc;
            Direccion = direccion;
            Telefono = telefono;
            Correo = correo;
            EsEmpresa = esEmpresa;
            FechaRegistro = fechaRegistro;
        }

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
