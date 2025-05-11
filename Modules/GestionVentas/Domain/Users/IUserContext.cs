using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Domain.Business;

namespace Ferreteria.Modules.GestionVentas.Domain.Users
{
    public interface IUserContext
    {
        UserId UserId { get; }

        BusinessId BusinessId { get; }
    }
}
