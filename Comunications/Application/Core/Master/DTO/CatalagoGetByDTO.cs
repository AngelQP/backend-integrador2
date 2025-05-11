using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Master.DTO
{
    public class CatalagoGetByDTO
    {
        public string CodigoAplicacion { get; set; }
        public string CodigoCatalogo { get; set; }
        public string CodigoDetalle { get; set; }
        public string Label { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string Descripcion { get; set; }
        public string CodigoSociedad { get; set; }
        public string CodigoPais { get; set; }
    }
}
