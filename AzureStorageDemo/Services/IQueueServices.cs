using System.Threading.Tasks;

namespace AzureStorageDemo.Services
{
    public interface IQueueServices
    {
        Task EnqueueAsync(string queueName, string message);
        Task<string> DequeueAsync(string queueName);
    }
}
