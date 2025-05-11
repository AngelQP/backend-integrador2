using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Mail.DTO
{
    public class SendRequest
    {
        //public MetaRequest Meta { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }

        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> CCO { get; set; }
        public List<FileAttached> FileAttachedList { get; set; }
    }

    //public class FileAttached
    //{
    //    public string FileName { get; set; }
    //    public byte[] Attached { get; set; }
    //}

    //public class MetaRequest
    //{
    //    public string Modifier { get; set; }
    //    public Guid Identifier { get; set; }
    //}
}
