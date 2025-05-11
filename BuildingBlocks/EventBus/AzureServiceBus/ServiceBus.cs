using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.EventBus.AzureServiceBus
{
    public class ServiceBus : IServiceBus
    {
        private readonly string _connectionString;

        public ServiceBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public QueueClient GetQueueClient(string queue)
        {
            return new QueueClient(_connectionString, queue);
        }

        public async Task PutOnQueue<T>(string queue, T message)
        {
            var client = new QueueClient(_connectionString, queue);
            var json = JsonSerializer.Serialize(message);

            await client.SendAsync(
                new Message(Encoding.UTF8.GetBytes(json))
            );

            await client.CloseAsync();

        }
    }
}
