using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentSearchEngine.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DocumentSearchEngine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public IndexModel(ILogger<IndexModel> logger,IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

        public List<ResultModel> Result { get; set; }
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                Result = QueryManager.Search(_hostEnvironment, Query);
            }
        }

        public IActionResult OnPost(string fileName)
        {
            Result = QueryManager.Search(_hostEnvironment, Query);

           
            string contentRootPath = _hostEnvironment.WebRootPath;
            return PhysicalFile(Path.Combine(contentRootPath, "uploads/"+fileName), "application/pdf", fileName);
        }
    }
}
