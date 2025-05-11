using Bigstick.BuildingBlocks.HttpClient;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.FileBlob;
using Ferreteria.Comunications.Application.Core.FileBlob.DTO;

namespace Ferreteria.Comunications.Infrastructure.Core
{
    public class FileService : IFileService
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _version;
        private string _urlBase { get; }

        public FileService(IHttpClientService httpClientService, string urlBase, string version)
        {
            this._httpClient = httpClientService;
            this._urlBase = urlBase;
            this._version = version;
        }

        public async Task<FileBlobDTO> UploadFile(string clientId, string app, string entity, IFormFile files, Stream streamContent)
        {
            var fileNameOnly = Path.GetFileName(files.FileName);
            return await _httpClient.SingleUploadOkAsync<FileBlobDTO>($"{this._urlBase}File/{clientId}/{app}/{entity}", streamContent, files.FileName, files.ContentType, "files" );
        
        }
    }
}
