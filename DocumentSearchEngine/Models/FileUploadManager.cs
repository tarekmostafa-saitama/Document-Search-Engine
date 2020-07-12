using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DocumentSearchEngine.Models
{
    public class FileUploadManager
    {

        public static string UploadSingleFile(IFormFile file, IWebHostEnvironment hostEnvironment)
        {
            var guid = Guid.NewGuid();
            var path = hostEnvironment.WebRootPath + "/uploads/" + guid + file.FileName;
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return guid+file.FileName;
        }
    }
}
