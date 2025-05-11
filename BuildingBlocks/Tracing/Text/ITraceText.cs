using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Tracing.Text
{
    public interface ITraceText
    {
        void RegistrarEvento(string message);
        void RegistrarEvento(List<string> values);
        void RegistrarEvento(ErrorMessage errorTemplate, params string[] values);
        void RegistrarEvento(ServiceMessage serviceMessage);
        void RegistrarEvento(Exception ex, params string[] values);
    }
}
