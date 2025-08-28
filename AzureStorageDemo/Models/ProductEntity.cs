namespace AzureStorageDemo.Models
{
    public class ProductEntity
    {
        public string PartitionKey { get; set; } = "General";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
