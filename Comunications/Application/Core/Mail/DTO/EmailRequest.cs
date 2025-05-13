using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Mail.DTO
{
    public class EmailRequest
    {
        public Meta Meta { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<string> TO { get; set; }
        public List<string> CC { get; set; }
        public List<string> CCO { get; set; }
        public List<FileAttachment> FileAttachedList { get; set; }
    }

    public class Meta
    {
        public string Modifier { get; set; }
        public Guid Identifier { get; set; }
    }

    public class FileAttachment
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; } // Base64-encoded content
        public string ContentType { get; set; }
    }
}
