using AzureStorageDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureStorageDemo.Services
{
    public interface IFileShareServices
    {
        Task<List<FileItemDto>> ListFilesAsync(string shareName, string directoryName);
        Task UploadFileAsync(string shareName, string directoryName, string fileName, byte[] fileContent);
    }
}
