using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail.DTO;

namespace Ferreteria.Comunications.Application.Core.Mail
{
    public interface IMailService
    {
        //Task<SendResponse> Send(MailMessage request);
        Task<RequestResult> Send(EmailRequest request);
        Task<string> Version();
        Task Passive();
    }
}
