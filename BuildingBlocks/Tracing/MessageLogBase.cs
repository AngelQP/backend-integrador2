using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Tracing
{
    public class MessageLogBase
    {
        public Guid Guid { get; set; }
        public string Servidor { get; set; }
        public string Aplicacion { get; set; }
        public DateTime Fechahora { get; set; }
        public string Usuario { get; set; }

        public string Values { get; set; }
    }

    public class ErrorMessage : MessageLogBase
    {
        public string Mensaje { get; set; }
        public string Inner { get; set; }
        public string Stack { get; set; }

    }

    public class ServiceMessage : MessageLogBase
    {
        public string Mensaje { get; set; }
        public string Metodo { get; set; }
    }
}
