using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentSearchEngine.Models;
using DocumentSearchEngine.Models.PageModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace DocumentSearchEngine.Pages
{
    public class UploadModel : PageModel
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public UploadFileModel UploadFileModel { get; set; }

        public string Message { get; set; }
        public UploadModel(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var fileName = FileUploadManager.UploadSingleFile(UploadFileModel.File, _hostEnvironment);

                var text = FileDataExtractor.Extract(_hostEnvironment.WebRootPath + "\\uploads\\" + fileName);

                FileIndexingManager.AddToIndex(_hostEnvironment, UploadFileModel.Title, text, fileName);

                Message = $"Document had been uploaded successfully.";
            }
            return Page();
        }
    }
}