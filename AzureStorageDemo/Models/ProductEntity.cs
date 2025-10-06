using Azure;
using Azure.Data.Tables;

namespace AzureStorageDemo.Models
{
    public class ProductEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Product";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
