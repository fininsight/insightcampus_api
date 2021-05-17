using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using Microsoft.AspNetCore.Authorization;
using DinkToPdf.Contracts;
using insightcampus_api.Utility;
using DinkToPdf;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassStudentController : ControllerBase
    {
        private readonly ClassStudentInterface _classstudent;
        private IConverter _converter;
        private readonly PdfInterface _pdf;

        public ClassStudentController(ClassStudentInterface classstudent, IConverter converter, PdfInterface pdf)
        {
            _classstudent = classstudent;
            _converter = converter;
            _pdf = pdf;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{class_seq}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int class_seq, int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;
            dataTableInputDto.class_seq = class_seq;

            return await _classstudent.Select(class_seq, dataTableInputDto);
        }

        
        [HttpPost("SendCertification/{class_seq}/{order_user_seq}")]
        public async Task<ActionResult> SendCertification(int class_seq, int order_user_seq)
        {
            var classstudent = await _pdf.SelectStudent(class_seq, order_user_seq);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = new PechkinPaperSize("260mm", "190mm"),
                Margins = new MarginSettings { Top = 10 },
                DPI = 300,
                DocumentTitle = "",
            };


            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = CertificationGenerator.GetHTMLString(classstudent),
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

            var filePath = Path.GetTempFileName();
            System.IO.File.WriteAllBytes(filePath, file);

            await _classstudent.SendCertification(class_seq, order_user_seq, filePath);
            return Ok();
        }

    }

}