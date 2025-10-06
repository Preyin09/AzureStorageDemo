using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureStorageDemo.Models;

namespace AzureStorageDemo.Services
{
    public class BlobServices : IBlobServices
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobServices(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        // Ensure container exists
        public async Task EnsureContainerExistsAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
        }

        // List all blobs
        public async Task<List<FileItemDto>> ListFilesAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var files = new List<FileItemDto>();
            await foreach (var blob in containerClient.GetBlobsAsync())
            {
                files.Add(new FileItemDto
                {
                    Name = blob.Name,
                    Size = blob.Properties.ContentLength ?? 0
                });
            }
            return files;
        }

        // Upload file
        public async Task UploadFileAsync(string containerName, string fileName, byte[] fileContent)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);
            using var ms = new MemoryStream(fileContent);
            await blobClient.UploadAsync(ms, overwrite: true);
        }
    }
}

