using AzureStorageDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IFileShareServices _fileShareServices;
        private const string ShareName = "contracts";
        private const string DirectoryName = "payments";

        public ContractsController(IFileShareServices fileShareServices)
        {
            _fileShareServices = fileShareServices;
        }

        public async Task<IActionResult> Index()
        {
            var files = await _fileShareServices.ListFilesAsync(ShareName, DirectoryName);
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                await _fileShareServices.UploadFileAsync(file, ShareName, DirectoryName);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
