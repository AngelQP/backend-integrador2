using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.EventBus.AzureServiceBus
{
    public interface IServiceBus
    {
        QueueClient GetQueueClient(string queue);
        Task PutOnQueue<T>(string queue, T message);
    }
}
