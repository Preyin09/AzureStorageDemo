namespace AzureStorageDemo.Services
{
    public class BlobItemDto
    {
        public string Name { get; internal set; }
        public long Size { get; internal set; }
        public string ContentType { get; internal set; }
    }
}