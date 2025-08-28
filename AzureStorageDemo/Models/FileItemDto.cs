﻿namespace AzureStorageDemo.Models
{
    public class FileItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;  // For blob container files
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; } = 0;
    }
}
