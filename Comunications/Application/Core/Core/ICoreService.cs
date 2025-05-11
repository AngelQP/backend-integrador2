using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.Core.DTO;

namespace Ferreteria.Comunications.Application.Core.Core
{
    public interface ICoreService
    {
        Task<IEnumerable<TipoCambioGetByFechaDTO>> TipoCambio(DateTime? fecha);
    }
}
