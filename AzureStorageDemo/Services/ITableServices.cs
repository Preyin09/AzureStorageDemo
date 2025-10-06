using AzureStorageDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureStorageDemo.Services
{
    public interface ITableServices
    {
        Task AddCustomerAsync(CustomerEntity customer);
        Task<List<CustomerEntity>> ListCustomersAsync();
        Task AddProductAsync(ProductEntity product);
        Task<List<ProductEntity>> ListProductsAsync();
    }
}
