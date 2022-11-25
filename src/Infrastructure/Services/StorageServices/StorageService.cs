using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Services.StorageServices
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;
        public StorageService(IStorage storage)
        {
            _storage = storage;
        }
        public Task DeleteAsync(string pathOrContainerName, string fileName)
            => _storage.DeleteAsync(pathOrContainerName, fileName);
        public string FileRename(string FileName)
            => _storage.FileRename(FileName);
        public FileType GetFileType(string fileType)
            =>_storage.GetFileType(fileType);
        public bool IsValidSize(long fileSize, int maxKb)
            => _storage.IsValidSize(fileSize, maxKb);
        public Task<(string path, string fileName)>UploadAsync(string path, IFormFile file)
            =>_storage.UploadAsync(path, file);

    }
}
