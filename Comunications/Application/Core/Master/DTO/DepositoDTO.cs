using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Master.DTO
{
    public class DepositoDTO
    {
        public string CodigoDeposito { get; set; }
        public string CodigoSucursal { get; set; }
        public string Nombre { get; set; }
        public string CodigoSunat { get; set; }
        public int Tipo { get; set; }
        public string PorDefecto { get; set; }
    }
}
