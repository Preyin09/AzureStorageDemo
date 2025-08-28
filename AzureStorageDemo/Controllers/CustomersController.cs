using AzureStorageDemo.Models;
using AzureStorageDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ITableServices _tableServices;

        public CustomersController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _tableServices.ListCustomersAsync();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerEntity customer)
        {
            if (ModelState.IsValid)
            {
                await _tableServices.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
