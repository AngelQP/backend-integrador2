using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Reports
{
    public interface IReporte
    {
        Task<byte[]> GenerarReporte<T>(IEnumerable<T> list, string tituloReporte, params string[] excluido);
    }
}
