using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureStorageDemo.Models;
using Microsoft.AspNetCore.Http;

namespace AzureStorageDemo.Services
{
    public class BlobServices : IBlobServices
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobServices(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<List<FileItemDto>> ListFilesAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Create container if it doesn't exist
            await containerClient.CreateIfNotExistsAsync();

            var items = new List<FileItemDto>();

            await foreach (var blob in containerClient.GetBlobsAsync())
            {
                items.Add(new FileItemDto
                {
                    Name = blob.Name,
                    Url = containerClient.GetBlobClient(blob.Name).Uri.ToString(),
                    Size = blob.Properties.ContentLength ?? 0,
                    ContentType = blob.Properties.ContentType
                });
            }

            return items;
        }


        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{file.FileName}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
    }
}
