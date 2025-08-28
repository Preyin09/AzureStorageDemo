using AzureStorageDemo.Models;
using Microsoft.AspNetCore.Http;

namespace AzureStorageDemo.Services
{
    public interface IFileShareServices
    {
        Task<List<FileItemDto>> ListFilesAsync(string shareName, string directoryName = "");
        Task<string> UploadFileAsync(IFormFile file, string shareName, string directoryName = "");
    }
}
