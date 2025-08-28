using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using AzureStorageDemo.Models;
using Microsoft.AspNetCore.Http;

namespace AzureStorageDemo.Services
{
    public class FileShareServices : IFileShareServices
    {
        private readonly ShareServiceClient _shareServiceClient;

        public FileShareServices(ShareServiceClient shareServiceClient)
        {
            _shareServiceClient = shareServiceClient;
        }

        public async Task<List<FileItemDto>> ListFilesAsync(string shareName, string directoryName = "")
        {
            var shareClient = _shareServiceClient.GetShareClient(shareName);
            var dirClient = string.IsNullOrEmpty(directoryName)
                ? shareClient.GetRootDirectoryClient()
                : shareClient.GetDirectoryClient(directoryName);

            var items = new List<FileItemDto>();
            await foreach (var file in dirClient.GetFilesAndDirectoriesAsync())
            {
                if (file.IsDirectory) continue;

                var fileClient = dirClient.GetFileClient(file.Name);
                var props = await fileClient.GetPropertiesAsync();

                items.Add(new FileItemDto
                {
                    Name = file.Name,
                    Size = props.Value.ContentLength,
                    ContentType = props.Value.ContentType
                });
            }

            return items;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string shareName, string directoryName = "")
        {
            var shareClient = _shareServiceClient.GetShareClient(shareName);
            var dirClient = string.IsNullOrEmpty(directoryName)
                ? shareClient.GetRootDirectoryClient()
                : shareClient.GetDirectoryClient(directoryName);

            await dirClient.CreateIfNotExistsAsync();

            var fileClient = dirClient.GetFileClient($"{DateTime.Now:yyyyMMdd_HHmmss}_{file.FileName}");
            using var stream = file.OpenReadStream();
            await fileClient.CreateAsync(stream.Length);
            await fileClient.UploadAsync(stream);

            return fileClient.Name;
        }
    }
}
