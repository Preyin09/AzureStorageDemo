
namespace AzureStorageDemo.Models
{
    public class AzureOptions
    {
        public string BlobContainer { get; set; }
        public string FileShare { get; set; }
        public string QueueOrders { get; set; }
        public string QueueStock { get; set; }
        public string? ConnectionString { get; internal set; }
    }
}