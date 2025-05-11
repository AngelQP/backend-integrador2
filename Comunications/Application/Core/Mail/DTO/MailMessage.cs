using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Mail.DTO
{
    public class MailMessage
    {
        public MailMessage()
        {
            this.To = new List<string>();
            this.CC = new List<string>();
            this.CCO = new List<string>();
            this.FileAttachedList = new List<FileAttached>();
        }

        public MailMessage(string subject, string body, bool isBodyHtml, List<string> to, List<string> cC, List<string> cCO, List<FileAttached> fileAttachedList)
        {
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;
            To = to;
            CC = cC;
            CCO = cCO;
            FileAttachedList = fileAttachedList;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }

        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> CCO { get; set; }
        public List<FileAttached> FileAttachedList { get; set; }
    }

    public class FileAttached
    {
        public string FileName { get; set; }
        public byte[] Attached { get; set; }
    }
}
