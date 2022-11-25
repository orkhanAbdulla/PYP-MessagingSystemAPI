using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base
{
    public interface IStorage
    {
        public Task<string> UploadAsync(string path, IFormFile file);
        void DeleteAsync(string pathOrContainerName, string fileName);
        public FileType GetFileType(string fileType);
        public string FileRename(string FileName);
        public bool IsValidSize(long fileSize, int maxKb);
    }
}
