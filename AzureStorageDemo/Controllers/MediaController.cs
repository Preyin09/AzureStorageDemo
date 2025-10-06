using AzureStorageDemo.Models;
using AzureStorageDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.Controllers
{
    public class MediaController : Controller
    {
        private readonly IBlobServices _blobServices;
        private readonly string ContainerName = "media"; // matches Azure container

        public MediaController(IBlobServices blobServices)
        {
            _blobServices = blobServices;
        }

        public async Task<IActionResult> Index()
        {
            await _blobServices.EnsureContainerExistsAsync(ContainerName);
            var files = await _blobServices.ListFilesAsync(ContainerName);
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                await _blobServices.UploadFileAsync(ContainerName, file.FileName, ms.ToArray());
            }
            return RedirectToAction("Index");
        }
    }
}
