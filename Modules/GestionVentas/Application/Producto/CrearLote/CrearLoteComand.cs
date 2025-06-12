using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.CrearLote
{
    public class CrearLoteComand : CommandBase<RequestResult>
    {
        public CrearLoteComand(
            int idProducto,
            string numeroLote,
            DateTime fechaIngreso,
            DateTime? fechaFabricacion,
            DateTime? fechaVencimiento,
            decimal cantidadInicial,
            decimal cantidadDisponible,
            decimal costoUnitario,
            string estadoRegistro,
            string usuarioCreacion,
            DateTime fechaCreacion,
            int? idProveedor,
            List<string> usuariosNotificados)
        {
            IdProducto = idProducto;
            NumeroLote = numeroLote;
            FechaIngreso = fechaIngreso;
            FechaFabricacion = fechaFabricacion;
            FechaVencimiento = fechaVencimiento;
            CantidadInicial = cantidadInicial;
            CantidadDisponible = cantidadDisponible;
            CostoUnitario = costoUnitario;
            EstadoRegistro = estadoRegistro;
            UsuarioCreacion = usuarioCreacion;
            FechaCreacion = fechaCreacion;
            IdProveedor = idProveedor;
            UsuariosNotificados = usuariosNotificados ?? new List<string>();
        }

        public int IdProducto { get; set; }
        public string NumeroLote { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal CantidadInicial { get; set; }
        public decimal CantidadDisponible { get; set; }
        public decimal CostoUnitario { get; set; }
        public string EstadoRegistro { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdProveedor { get; set; }
        public List<string> UsuariosNotificados { get; set; } = new List<string>();

    }
}