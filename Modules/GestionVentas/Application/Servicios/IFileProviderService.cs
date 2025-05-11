using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Servicios
{
    public interface IFileProviderService
    {
        Task<Stream> DownloadStreamingAsync(string containerName, string fileNameAzure);
    }
}
