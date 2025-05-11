using Bigstick.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Data;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.DataAccess
{
    internal class MSqlTramarsaConnectionFactory : MsSqlConnectionFactory, ITramarsaConnectionFactory
    {
        public MSqlTramarsaConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }
}
