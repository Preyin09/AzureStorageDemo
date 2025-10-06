using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using AzureStorageDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureStorageDemo.Services
{
    public class FileShareServices : IFileShareServices
    {
        private readonly ShareServiceClient _shareServiceClient;

        public FileShareServices(ShareServiceClient shareServiceClient)
        {
            _shareServiceClient = shareServiceClient;
        }

        public async Task<List<FileItemDto>> ListFilesAsync(string shareName, string directoryName)
        {
            var result = new List<FileItemDto>();

            var shareClient = _shareServiceClient.GetShareClient(shareName);
            var directoryClient = shareClient.GetDirectoryClient(directoryName);

            await foreach (var item in directoryClient.GetFilesAndDirectoriesAsync())
            {
                if (!item.IsDirectory)
                {
                    var fileClient = directoryClient.GetFileClient(item.Name);
                    var properties = await fileClient.GetPropertiesAsync();

                    result.Add(new FileItemDto
                    {
                        Name = item.Name,
                        ContentLength = properties.Value.ContentLength,
                        LastModified = properties.Value.LastModified.DateTime
                    });
                }
            }

            return result;
        }

        public async Task UploadFileAsync(string shareName, string directoryName, string fileName, byte[] fileContent)
        {
            var shareClient = _shareServiceClient.GetShareClient(shareName);
            var directoryClient = shareClient.GetDirectoryClient(directoryName);
            var fileClient = directoryClient.GetFileClient(fileName);

            // Create file with correct size
            await fileClient.CreateAsync(fileContent.Length);

            // Upload content
            using (var stream = new System.IO.MemoryStream(fileContent))
            {
                await fileClient.UploadAsync(stream); // no 'overwrite' parameter needed
            }
        }

    }
}

