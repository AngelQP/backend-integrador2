using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO
{
    public class MetaResponse
    {
        public string ErrCode { get; set; } = "";
        public string Message { get; set; } = "";
        public Guid Identifier { get; set; }
    }
}
