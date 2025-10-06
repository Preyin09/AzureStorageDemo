using AzureStorageDemo.Models;

namespace AzureStorageDemo.Services
{
    public interface IBlobServices
    {
        /// <summary>
        /// Ensures the container exists; creates it if it doesn't.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task EnsureContainerExistsAsync(string containerName);

        /// <summary>
        /// Lists all files in the specified container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns>List of FileItemDto</returns>
        Task<List<FileItemDto>> ListFilesAsync(string containerName);

        /// <summary>
        /// Uploads a file to the specified container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        Task UploadFileAsync(string containerName, string fileName, byte[] fileContent);
    }
}
