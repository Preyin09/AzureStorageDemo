using AzureStorageDemo.Models;
using AzureStorageDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ITableServices _tableServices;

        public ProductsController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _tableServices.ListProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductEntity product)
        {
            if (ModelState.IsValid)
            {
                await _tableServices.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
