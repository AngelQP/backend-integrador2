using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Core.DTO
{
    public class TipoCambioGetByFechaDTO
    {
        public string CodigoMoneda { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambioVenta { get; set; }
        public decimal TipCaC { get; set; }
        public decimal TipCCC { get; set; }
        public decimal TipCCV { get; set; }
        public DateTime FecPrc { get; set; }
        public DateTime FecAct { get; set; }
    }
}
