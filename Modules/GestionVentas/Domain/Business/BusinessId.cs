using Bigstick.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Business
{
    public class BusinessId : ValueObject
    {
        private BusinessId(string codigoSociedad, string codigoNegocio)
        {
            CodigoNegocio = codigoNegocio;
            CodigoSociedad = codigoSociedad;
        }

        public string CodigoSociedad { get; }
        public string CodigoNegocio { get; }

        public static BusinessId Create(string codigoSociedad, string codigoNegocio)
        {
            return new(codigoSociedad, codigoNegocio);
        }
    }
}
