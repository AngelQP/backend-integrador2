using Bigstick.BuildingBlocks.HttpClient.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.HttpClient
{
    public interface IHttpClientService
    {
        event EventSendingHandler MessageSending;

        event EventSentHandler MessageSent;

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, AuthoToken token, Dictionary<string, string> headers);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        Task<HttpResponseMessage> GetAsync(string url, AuthoToken token = null, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> PostAsync<T>(string url, T resource, AuthoToken token = null, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> PatchAsync<T>(string url, T resource, AuthoToken token = null, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> PutAsync<T>(string url, T resource, AuthoToken token = null, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> DeleteAsync(string url, AuthoToken token = null, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> DeleteAsync<T>(string url, T resource, AuthoToken token = null, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> UploadAsync(string url, Dictionary<string, byte[]> content, AuthoToken token = null, Dictionary<string, string> headers = null,
            string contentType = "image/jpeg", IDictionary<string, string> textContent = null);

        Task<System.Net.Http.HttpResponseMessage> UploadAsync(string url, Stream content, string filename, string contentType, string keyform = null);
    }
}
