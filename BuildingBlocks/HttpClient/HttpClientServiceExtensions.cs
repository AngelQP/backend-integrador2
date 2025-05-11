using Bigstick.BuildingBlocks.HttpClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.HttpClient
{
    public static class HttpClientServiceExtensions
    {

        public static async Task<TResult> GetOkAsync<TResult>(this IHttpClientService httpClientService, string url, AuthoToken token = null, Dictionary<string, string> headers = null) 
        {
            var response = await httpClientService.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        public static async Task<TResult> PostOkAsync<TResult>(this IHttpClientService httpClientService, string url, object resource, AuthoToken token = null, Dictionary<string, string> headers = null) 
        {
            var response = await httpClientService.PostAsync(url, resource, token, headers);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }
      
        public static async Task<TResult> PatchOkAsync<TResult>(this IHttpClientService httpClientService, string url, object resource, AuthoToken token = null, Dictionary<string, string> headers = null) 
        {
            var response = await httpClientService.PatchAsync(url, resource);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        public static async Task<TResult> PutOkAsync<TResult>(this IHttpClientService httpClientService, string url, object resource, AuthoToken token = null, Dictionary<string, string> headers = null) 
        {
            var response = await httpClientService.PutAsync(url, resource);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        public static async Task<TResult> SingleUploadOkAsync<TResult>(this IHttpClientService httpClientService, string url, System.IO.Stream content, string filename, string contentType, string keyform = null)
        {
            var response = await httpClientService.UploadAsync(url, content, filename, contentType, keyform);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }
    }
}
