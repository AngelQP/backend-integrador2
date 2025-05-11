using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Application.Core.Mail.DTO
{
    public class SendResponse
    {
        public bool Success { get; set; } = false;
        public MetaResponse Meta { get; set; }
    }

    public class MetaResponse
    {
        public string ErrCode { get; set; } = "";
        public string Message { get; set; } = "";
        public int Total { get; set; }
        public Guid Identifier { get; set; }
    }
}
