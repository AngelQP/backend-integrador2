using Bigstick.BuildingBlocks.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Comunications.Application.Core.Mail.DTO;

namespace Ferreteria.Comunications.Infrastructure.Core
{
    public class MailService : IMailService
    {
        private readonly IHttpClientService _httpClient;
        private string _urlBase { get; }

        public MailService(IHttpClientService httpClientService, string urlBase)
        {
            this._httpClient = httpClientService;
            this._urlBase = urlBase;
        }
        public async Task<SendResponse> Send(MailMessage request)
        {
            return await _httpClient
                       .PostOkAsync<SendResponse>($"{_urlBase}send", request);
        }

        public async Task<string> Version()
        {
            return await _httpClient
                        .GetOkAsync<string>($"{_urlBase}version");
        }

        public async Task Passive()
        {
            Thread back = new Thread(() => {
                int segundos = 5;
                while (segundos > 0)
                {
                    Thread.Sleep(1000);
                    segundos--;
                }

                _httpClient
                        .GetOkAsync<string>($"{_urlBase}passive");
            });
            back.IsBackground = true;
            back.Start();
        }
    }
}
