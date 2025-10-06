using AzureStorageDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureStorageDemo.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IFileShareServices _fileShareService;

        public ContractsController(IFileShareServices fileShareService)
        {
            _fileShareService = fileShareService;
        }

        public async Task<IActionResult> Index()
        {
            string shareName = "contracts";      // replace with your actual share name
            string directoryName = "documents";  // replace with your actual folder, "" for root

            var files = await _fileShareService.ListFilesAsync(shareName, directoryName);

            return View(files);
        }
    }
}
