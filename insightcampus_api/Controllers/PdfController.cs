using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using insightcampus_api.Utility;
using insightcampus_api.Dao;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IO;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private IConverter _converter;
        private readonly PdfInterface _pdf;

        public PdfController(IConverter converter, PdfInterface pdf)
        {
            _converter = converter;
            _pdf = pdf;
        }

        [HttpGet("addfare/{addfare_seq}")]
        public async Task<IActionResult> CreatePDF(int addfare_seq)
        {
            // sudo apt-get install libgdiplus

            var addfare = await _pdf.Select(addfare_seq);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DPI = 300,
                DocumentTitle = "",
            };
            

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(addfare),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "style.css") },
                HeaderSettings = { FontName = "NanumGothic", FontSize = 9, Right = "", Line = false },
                FooterSettings = { FontName = "NanumGothic", FontSize = 9, Right = "", Line = false }
            };
            
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", addfare.name + "님_지급명세서_" + addfare.addfare_date.ToString("yyyy-MM-dd") + ".pdf"); // for downloading as sample.pdf
        }


        

        [HttpGet("certification")]
        public async Task<IActionResult> CreateEduCertificationPDF()
        {

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DPI = 300,
                DocumentTitle = "",
            };


            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetEduCertificationHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "edu_certification_style.css") },
                HeaderSettings = { FontName = "NanumGothic", FontSize = 9, Right = "", Line = false },
                FooterSettings = { FontName = "NanumGothic", FontSize = 9, Right = "", Line = false }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            
            return File(file, "application/pdf", "test" + " 님_교육수료증_" + ".pdf");
            
        }
    }
}
