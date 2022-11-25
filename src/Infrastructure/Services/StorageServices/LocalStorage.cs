using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Domain.Enums;
using MessagingSystemApp.Infrastructure.Services.StorageServices.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq.Expressions;

namespace MessagingSystemApp.Infrastructure.Services.StorageServices
{
    public class LocalStorage : Storage, ILocalStorage
    {
        public async Task DeleteAsync(string path, string fileName)
        {
            string DeleteFile = Path.Combine(Directory.GetCurrentDirectory() + $"/wwwroot/{path}/{fileName}");
            if (File.Exists(DeleteFile))
            {
                File.Delete(DeleteFile);
            }
        }
        public async Task<(string path, string fileName)> UploadAsync(string path, IFormFile file)
        {
            string newFilename = FileRename(file.FileName);
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory() + $"/wwwroot/{path}");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            string rooting = Path.Combine(uploadPath, newFilename);
            using (FileStream stream = new FileStream(rooting, FileMode.CreateNew))
            {
               await file.CopyToAsync(stream);
            }
            return (rooting,newFilename);
        }   
    }
}
