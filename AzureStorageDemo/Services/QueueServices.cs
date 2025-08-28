using Azure.Storage.Queues;

namespace AzureStorageDemo.Services
{
    public class QueueServices : IQueueServices
    {
        private readonly QueueServiceClient _queueClient;

        public QueueServices(QueueServiceClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task EnqueueAsync(string queueName, string message)
        {
            var queue = _queueClient.GetQueueClient(queueName);
            await queue.CreateIfNotExistsAsync();
            await queue.SendMessageAsync(message);
        }

        public async Task<string?> DequeueAsync(string queueName)
        {
            var queue = _queueClient.GetQueueClient(queueName);
            var msg = await queue.ReceiveMessageAsync();
            if (msg.Value != null)
            {
                await queue.DeleteMessageAsync(msg.Value.MessageId, msg.Value.PopReceipt);
                return msg.Value.MessageText;
            }
            return null;
        }
    }
}
