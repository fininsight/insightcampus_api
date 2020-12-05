using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using insightcampus_api.Utility;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IO;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployProofController : ControllerBase
    {
        private readonly EmployProofInterface _employproof;
        private IConverter _converter;

        public EmployProofController(IConverter converter, EmployProofInterface __employproof)
        {
            _converter = converter;
            _employproof = __employproof;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _employproof.Select(dataTableInputDto);
        }

        [HttpGet("{class_seq}")]
        public async Task<ActionResult<EmployProofModel>> Get(int employee_proof_seq)
        {
            return await _employproof.Select( employee_proof_seq );
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployProofModel employproof)
        {
            employproof.reg_date = DateTime.Now;
            await _employproof.Add(employproof);
            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{employee_proof_seq}")]
        public async Task<ActionResult> Put([FromBody] EmployProofModel employproof, int employee_proof_seq)
        {
            employproof.upd_date = DateTime.Now;
            employproof.employee_proof_seq = employee_proof_seq;
            await _employproof.Update(employproof);
            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            EmployProofModel employproof = new EmployProofModel
            {
                employee_proof_seq = seq
            };
            await _employproof.Delete(employproof);
            return Ok();
        }

        [HttpGet("pdf/{employee_proof_seq}")]
        public async Task<IActionResult> CreateProofPDF(int employee_proof_seq)
        {
            // sudo apt-get install libgdiplus

            var employproof = await _employproof.Select(employee_proof_seq);
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
                HtmlContent = TemplateGenerator.GetProofHTMLString(employproof),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "proof_style.css") },
                HeaderSettings = { FontName = "NanumGothic", FontSize = 9, Right = "", Line = false },
                FooterSettings = { FontName = "NanumGothic", FontSize = 9, Right = "", Line = false }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf", "test" + " 님_재직증명서_" + ".pdf"); // for downloading as sample.pdf
        }

    }
}
