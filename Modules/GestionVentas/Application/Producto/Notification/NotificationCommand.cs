using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.Notification
{
    public class NotificationCommand : CommandBase<RequestResult>
    {
        public NotificationCommand(string correo, int idProducto)
        {
            Correo = correo;
            IdProducto = idProducto;

        }

        public string Correo { get; set; }
        public int IdProducto { get; set; }
    }
}