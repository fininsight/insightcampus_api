using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    public class Fallback : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
