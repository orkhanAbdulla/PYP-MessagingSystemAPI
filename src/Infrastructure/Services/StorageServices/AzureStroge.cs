﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
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

        public async Task DeleteAsync(string ContainerName, string fileName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            if (_containerClient.GetBlobs().Any(b => b.Name == fileName))
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.DeleteAsync();
            }
        }

        public async Task<(string path,string fileName)> UploadAsync(string container, IFormFile file)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
            await _containerClient.CreateIfNotExistsAsync();
            await _containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            string newFilename = FileRename(file.FileName);
            BlobClient blogClient=_containerClient.GetBlobClient(newFilename);
            await blogClient.UploadAsync(file.OpenReadStream());
            string rooting = AzureStorageUrl + container + "/" + newFilename;
            return (rooting,newFilename);
        }

    }
}
