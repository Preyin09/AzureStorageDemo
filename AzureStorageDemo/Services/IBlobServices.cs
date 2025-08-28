using AzureStorageDemo.Models;
using Microsoft.AspNetCore.Http;

namespace AzureStorageDemo.Services
{
    public interface IBlobServices
    {
        Task<List<FileItemDto>> ListFilesAsync(string containerName);
        Task<string> UploadFileAsync(IFormFile file, string containerName);
    }
}
