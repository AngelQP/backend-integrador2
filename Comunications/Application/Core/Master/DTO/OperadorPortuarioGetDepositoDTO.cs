using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Master.DTO
{
    public class OperadorPortuarioGetDepositoDTO
    {
        public string CodigoOperadorPortuario { get; set; }
        public string Nombre { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoSucursal { get; set; }
        public string RUC { get; set; }
        public string CodigoDeposito { get; set; }
        public int Prioridad { get; set; }
    }
}
