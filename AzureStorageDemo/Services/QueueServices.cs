using Azure.Storage.Queues;
using System;
using System.Threading.Tasks;

namespace AzureStorageDemo.Services
{
    public class QueueServices : IQueueServices
    {
        private readonly QueueServiceClient _queueServiceClient;

        public QueueServices(QueueServiceClient queueServiceClient)
        {
            _queueServiceClient = queueServiceClient;
        }

        public async Task EnqueueAsync(string queueName, string message)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
        }

        public async Task<string> DequeueAsync(string queueName)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            var msg = await queueClient.ReceiveMessageAsync();

            if (msg.Value != null)
            {
                await queueClient.DeleteMessageAsync(msg.Value.MessageId, msg.Value.PopReceipt);
                return msg.Value.MessageText;
            }

            return null;
        }
    }
}
