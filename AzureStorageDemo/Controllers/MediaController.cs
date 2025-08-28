using AzureStorageDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.Controllers
{
    public class MediaController : Controller
    {
        private readonly IBlobServices _blobServices;
        private const string ContainerName = "media-files";

        public MediaController(IBlobServices blobServices)
        {
            _blobServices = blobServices;
        }

        public async Task<IActionResult> Index()
        {
            var files = await _blobServices.ListFilesAsync(ContainerName);
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                await _blobServices.UploadFileAsync(file, ContainerName);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
