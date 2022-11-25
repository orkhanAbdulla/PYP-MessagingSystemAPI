using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Services.StorageServices.Base
{
    public class Storage
    {
        public string FileRename(string FileName)
        {
            return Guid.NewGuid().ToString() + FileName;
        }
        public FileType GetFileType(string fileType)
        {
            if (fileType.ToUpper().Contains(FileType.PNG.ToString().ToUpper())) return FileType.PNG;
            if (fileType.ToUpper().Contains(FileType.JPEG.ToString().ToUpper())) return FileType.JPEG;
            if (fileType.ToUpper().Contains(FileType.GIF.ToString().ToUpper())) return FileType.GIF;
            if (fileType.ToUpper().Contains(FileType.PDF.ToString().ToUpper())) return FileType.PDF;
            if (fileType.ToUpper().Contains(FileType.SVG.ToString().ToUpper())) return FileType.SVG;
            if (fileType.ToUpper().Contains(FileType.TXT.ToString().ToUpper())) return FileType.TXT;
            if (fileType.ToUpper().Contains(FileType.XLS.ToString().ToUpper())) return FileType.XLS;
            if (fileType.ToUpper().Contains(FileType.XLSX.ToString().ToUpper())) return FileType.XLSX;
            return 0;
        }
        public bool IsValidSize(long fileSize, int maxKb)
        {
            return fileSize / 1024 <= maxKb;
        }
    }
}
