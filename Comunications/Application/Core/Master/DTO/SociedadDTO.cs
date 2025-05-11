using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Master.DTO
{
    public class SociedadDTO
    {
        public string CodigoSociedad { get; set; }
        public string Nombre { get; set; }
        public string RUC { get; set; }
        public string CodigoPais { get; set; }
        public int EstadoRegistro { get; set; }
    }
}
