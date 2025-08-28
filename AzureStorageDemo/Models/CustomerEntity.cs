namespace AzureStorageDemo.Models
{
    public class CustomerEntity
    {
        public string PartitionKey { get; set; } = "General";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
