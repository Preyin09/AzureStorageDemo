using AzureStorageDemo.Models;

namespace AzureStorageDemo.Services
{
    public interface ITableServices
    {
        // Customers
        Task<List<CustomerEntity>> ListCustomersAsync();
        Task AddCustomerAsync(CustomerEntity customer);

        // Products
        Task<List<ProductEntity>> ListProductsAsync(string partitionKey = "General");
        Task AddProductAsync(ProductEntity product);
    }
}
