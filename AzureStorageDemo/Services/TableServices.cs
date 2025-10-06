using Azure.Data.Tables;
using AzureStorageDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageDemo.Services
{
    public class TableServices : ITableServices
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly TableClient _customerTable;
        private readonly TableClient _productTable;

        public TableServices(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
            _customerTable = _tableServiceClient.GetTableClient("Customers");
            _customerTable.CreateIfNotExists();
            _productTable = _tableServiceClient.GetTableClient("Products");
            _productTable.CreateIfNotExists();
        }

        public async Task AddCustomerAsync(CustomerEntity customer)
        {
            await _customerTable.AddEntityAsync(customer);
        }

        public async Task<List<CustomerEntity>> ListCustomersAsync()
        {
            var entities = _customerTable.Query<CustomerEntity>().ToList();
            return entities;
        }

        public async Task AddProductAsync(ProductEntity product)
        {
            await _productTable.AddEntityAsync(product);
        }

        public async Task<List<ProductEntity>> ListProductsAsync()
        {
            var entities = _productTable.Query<ProductEntity>().ToList();
            return entities;
        }
    }
}
