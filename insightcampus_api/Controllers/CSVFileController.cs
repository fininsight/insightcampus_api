using System;
using System.IO;
using System.Text;
using insightcampus_api.Dao;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel = Microsoft.Office.Interop.Excel;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVFileController : ControllerBase
    {
        private readonly CSVFileInterface _csv;

        public CSVFileController(CSVFileInterface csv)
        {
            _csv = csv;
        }

       [HttpGet]
        public IActionResult Products()
        {
            //var data = _csv.ReturnData();

            var stream = new MemoryStream();
           
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "export_" + DateTime.Now + ".xlsx";
            

            var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Contents");
            _csv.ReturnData(worksheet);

                        
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
            




        }

    }
}