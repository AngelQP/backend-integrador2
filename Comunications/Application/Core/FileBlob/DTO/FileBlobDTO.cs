using Bigstick.BuildingBlocks.Application.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.FileBlob.DTO
{
    public class FileBlobDTO
    {
        public string ClientId { get; set; }

        public string ApplicationId { get; set; }

        public string EntityOwner { get; set; }

        public string IPAddress { get; set; }

        public List<UploadItemCommand> Items { get; set; }

        public class UploadItemCommand
        {
            public Guid Id { get; set; }

            public string FileName { get; set; }

            public string ContentDisposition { get; set; }

            public string ContentType { get; set; }

            public long Length { get; set; }



        }
    }
}
