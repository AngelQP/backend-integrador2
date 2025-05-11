using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Master.DTO
{
    public class NotificacionDTO
    {
        public int IdNotificacion { get; set; }
        public string Codigo { get; set; }
        public string De { get; set; }
        public string Para { get; set; }
        public string CC { get; set; }
        public string CCO { get; set; }
        public string Asunto { get; set; }
        public string Body { get; set; }
        public bool Html { get; set; }
        public string CodigoLinea { get; set; }
        public string CodigoAplicacion { get; set; }
        public string Descripcion { get; set; }
    }
}
