using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Contract;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CommandBase<>).Assembly;
    }
}
