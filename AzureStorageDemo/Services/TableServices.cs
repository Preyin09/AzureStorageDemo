using Azure;
using Azure.Data.Tables;
using AzureStorageDemo.Models;

namespace AzureStorageDemo.Services
{
    public class TableServices : ITableServices
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly string _customersTableName = "Customers";
        private readonly string _productsTableName = "Products";

        public TableServices(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        // ------------------- Customers -------------------
        public async Task<List<CustomerEntity>> ListCustomersAsync()
        {
            var tableClient = _tableServiceClient.GetTableClient(_customersTableName);
            await tableClient.CreateIfNotExistsAsync();

            var results = new List<CustomerEntity>();
            await foreach (var entity in tableClient.QueryAsync<TableEntity>())
            {
                results.Add(new CustomerEntity
                {
                    PartitionKey = entity.PartitionKey,
                    RowKey = entity.RowKey,
                    Name = entity.GetString("Name"),
                    Email = entity.GetString("Email"),
                    Phone = entity.GetString("Phone")
                });
            }

            return results;
        }

        public async Task AddCustomerAsync(CustomerEntity customer)
        {
            var tableClient = _tableServiceClient.GetTableClient(_customersTableName);
            await tableClient.CreateIfNotExistsAsync();

            var tableEntity = new TableEntity(customer.PartitionKey, customer.RowKey)
            {
                { "Name", customer.Name },
                { "Email", customer.Email },
                { "Phone", customer.Phone }
            };

            await tableClient.AddEntityAsync(tableEntity);
        }

        // ------------------- Products -------------------
        public async Task<List<ProductEntity>> ListProductsAsync(string partitionKey = "General")
        {
            var tableClient = _tableServiceClient.GetTableClient(_productsTableName);
            await tableClient.CreateIfNotExistsAsync();

            var results = new List<ProductEntity>();
            await foreach (var entity in tableClient.QueryAsync<TableEntity>(e => e.PartitionKey == partitionKey))
            {
                results.Add(new ProductEntity
                {
                    PartitionKey = entity.PartitionKey,
                    RowKey = entity.RowKey,
                    ProductName = entity.GetString("Name"),
                    Description = entity.GetString("Description"),
                    Price = entity.GetDouble("Price") ?? 0
                });
            }

            return results;
        }

        public async Task AddProductAsync(ProductEntity product)
        {
            var tableClient = _tableServiceClient.GetTableClient(_productsTableName);
            await tableClient.CreateIfNotExistsAsync();

            var tableEntity = new TableEntity(product.PartitionKey, product.RowKey)
            {
                { "Name", product.ProductName },
                { "Description", product.Description },
                { "Price", product.Price }
            };

            await tableClient.AddEntityAsync(tableEntity);
        }
    }
}
