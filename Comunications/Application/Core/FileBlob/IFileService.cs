using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.FileBlob.DTO;

namespace Ferreteria.Comunications.Application.Core.FileBlob
{
    public interface IFileService
    {
        Task<FileBlobDTO> UploadFile(string clientId, string app, string entity, IFormFile files, Stream streamContent);
    }
}
