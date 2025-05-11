using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.HttpClient
{
    public class HttpClientService : IHttpClientService
    {
        protected virtual System.Net.Http.HttpClient GetHttpClient()
        {
            return new System.Net.Http.HttpClient();
        }

        private async Task<HttpResponseMessage> SendWithBody<T>(string url, T resource, HttpMethod method, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (method != HttpMethod.Get && resource != null)

                request.Content = new ObjectContent<T>(resource, new JsonMediaTypeFormatter() { });

            
            var response = await this.SendAsync(request, token, headers);

            return response;
        }

        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, Model.AuthoToken token, Dictionary<string, string> headers)
        {
      
            if (token != null)
                request.Headers.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, (string)item.Value);
                }
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var client = GetHttpClient())
            {
                this.OnMessageSending(new EventHttpSendingArgs(client, request));

                var response = await client.SendAsync(request);

                this.OnMessageSent(new EventHttpSentArgs(client, request, response));

                return response;
            }
        }

        public async Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request)
        {
            return await SendAsync(request, null, null);
        }

        public async Task<System.Net.Http.HttpResponseMessage> GetAsync(string url, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            return await SendWithBody(url, default(object), HttpMethod.Get, token, headers);
        }

        public async Task<System.Net.Http.HttpResponseMessage> PostAsync<T>(string url, T resource, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            return await SendWithBody(url, resource, HttpMethod.Post, token, headers);
        }

        public async Task<System.Net.Http.HttpResponseMessage> PatchAsync<T>(string url, T resource, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            return await SendWithBody(url, resource, HttpMethod.Patch, token, headers);
        }

        public async Task<System.Net.Http.HttpResponseMessage> PutAsync<T>(string url, T resource, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            return await SendWithBody(url, resource, HttpMethod.Put, token, headers);
        }

        public async Task<System.Net.Http.HttpResponseMessage> DeleteAsync(string url, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            return await SendWithBody(url, default(object), HttpMethod.Delete, token, headers);
        }

        public async Task<System.Net.Http.HttpResponseMessage> DeleteAsync<T>(string url, T resource, Model.AuthoToken token = null, Dictionary<string, string> headers = null)
        {
            return await SendWithBody(url, resource, HttpMethod.Delete, token, headers);
        }

        public async Task<System.Net.Http.HttpResponseMessage> UploadAsync(string url, Dictionary<string, byte[]> content, Model.AuthoToken token = null, Dictionary<string, string> headers = null, string contentType = "image/jpeg", IDictionary<string, string> textContent = null)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                if (content != null)
                {
                    foreach (var item in content)
                    {
                        var fileContent1 = new ByteArrayContent(item.Value);
                        fileContent1.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = item.Key
                        };
                        fileContent1.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                        multipartFormContent.Add(fileContent1);
                    }
                }

                if (textContent != null)
                {
                    foreach (var item in textContent)
                    {
                        var text = new StringContent(item.Value, Encoding.UTF8);
                        text.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = item.Key
                        };
                        multipartFormContent.Add(text);
                    }
                }
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = multipartFormContent;

                var response = await this.SendAsync(request, token, headers);
                return response;
            }
        }

        public async Task<System.Net.Http.HttpResponseMessage> UploadAsync(string url, Stream content, string filename, string contentType, string keyform = null)
        {
            
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                var fileContent1 = new StreamContent(content);
                fileContent1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = keyform,
                    FileName = filename
                };
                fileContent1.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                multipartFormContent.Add(fileContent1, keyform);

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = multipartFormContent;

                var response = await this.SendAsync(request, null, null);
                return response;

            }
        }

        protected void OnMessageSending(EventHttpSendingArgs e)
        {
            var handler = MessageSending;

            if (handler != null)

                handler.Invoke(this, e);
        }

        protected void OnMessageSent(EventHttpSentArgs e)
        {
            var handler = MessageSent;

            if (handler != null)

                handler.Invoke(this, e);
        }

        public event EventSendingHandler MessageSending;

        public event EventSentHandler MessageSent;

    }
}
