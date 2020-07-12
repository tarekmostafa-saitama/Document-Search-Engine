using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DocumentSearchEngine.Models.PageModels
{
    public class UploadFileModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
