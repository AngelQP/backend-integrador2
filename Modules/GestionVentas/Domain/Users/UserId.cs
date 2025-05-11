using Bigstick.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Users
{
    public class UserId : ValueObject
    {
        public UserId(string value, string codigSap)
        {
            Value = value;
            CodigoSap = codigSap;
        }

        public string Value { get; }

        public string CodigoSap { get; }
    }
}
