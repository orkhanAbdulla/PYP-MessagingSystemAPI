using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Infrastructure.Services.StorageServices.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Services.StorageServices
{
    public class AzureStroge : Storage, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _containerClient;
        readonly string AzureStorageUrl;

        public AzureStroge(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
            AzureStorageUrl = configuration["AzureStorageUrl"];
        }

        public void DeleteAsync(string ContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAsync(string container, IFormFile file)
        {
            var fileFolder = GetFileType(file.ContentType).ToString().ToLower();
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
            await _containerClient.CreateIfNotExistsAsync();
            await _containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            string newFilename = FileRename(file.FileName);
            string path = fileFolder + "/" + newFilename;
            BlobClient blogClient=_containerClient.GetBlobClient(path);
            await blogClient.UploadAsync(file.OpenReadStream());
            var rooting = AzureStorageUrl+container+"/"+path;
            return rooting;
        }
    }
}
