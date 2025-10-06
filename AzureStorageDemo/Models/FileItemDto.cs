using System;

namespace AzureStorageDemo.Models
{
    public class FileItemDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public long ContentLength { get; set; }
        public DateTime LastModified { get; set; }
    }
}
