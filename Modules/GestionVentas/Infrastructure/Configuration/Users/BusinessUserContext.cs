using Bigstick.BuildingBlocks.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Domain.Business;
using Ferreteria.Modules.GestionVentas.Domain.Users;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Users
{
    public class BusinessUserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public BusinessUserContext(IExecutionContextAccessor executionContextAccessor)
        {
            this._executionContextAccessor = executionContextAccessor;
        }
        public BusinessId BusinessId => BusinessId.Create("301", "3013");

        public UserId UserId => new UserId(_executionContextAccessor.UserId, _executionContextAccessor.CodigoSap);
    }
}
