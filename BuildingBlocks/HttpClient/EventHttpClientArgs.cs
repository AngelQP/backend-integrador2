using System;
using System.Net.Http;

namespace Bigstick.BuildingBlocks.HttpClient
{
    public delegate void EventSendingHandler(object sender, EventHttpSendingArgs args);

    public delegate void EventSentHandler(object sender, EventHttpSentArgs args);
    

    public class EventHttpSendingArgs : EventArgs
    {
        public System.Net.Http.HttpClient Client { get; private set; }

        public HttpRequestMessage Message { get; private set; }

        public EventHttpSendingArgs(System.Net.Http.HttpClient client, HttpRequestMessage message)
        {
            this.Client = client;

            this.Message = message;
        }
    }

    public class EventHttpSentArgs : EventArgs
    {
        public System.Net.Http.HttpClient Client { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public HttpResponseMessage Response { get; private set; }

        public EventHttpSentArgs(System.Net.Http.HttpClient client, HttpRequestMessage request, HttpResponseMessage response)
        {
            this.Client = client;

            this.Request = request;

            this.Response = response;
        }
    }
}
