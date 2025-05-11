using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Master.DTO
{
    public class CuentaCorrienteGetByBancoDTO
    {
        public string CodigoSociedad { get; set; }
        public string CodigoBanco { get; set; }
        public string Banco { get; set; }
        public string CuentaCorriente { get; set; }
        public string Moneda { get; set; }
    }
}
