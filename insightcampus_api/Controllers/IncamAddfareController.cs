using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncamAddfareController : ControllerBase
    {
        private readonly IncamAddfareInterface _incamAddfare;

        public IncamAddfareController(IncamAddfareInterface incamAddfare)
        {
            _incamAddfare = incamAddfare;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{size:int}/{pageNumber:int}")]
        public async Task<ActionResult<DataTableOutDto>> Get([FromQuery(Name = "f")] string f, int size, int pageNumber)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _incamAddfare.Select(dataTableInputDto, filters);
        }

        [HttpGet("excel")]
        public async Task<IActionResult> getExcel([FromQuery(Name = "f")] string f)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            var result = await _incamAddfare.SelectExcel(filters);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "내부정산.xlsx";

     
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell(1, 1).Value = "정산서 No.";
                worksheet.Cell(1, 2).Value = "정산일자";
                worksheet.Cell(1, 3).Value = "원청사";
                worksheet.Cell(1, 4).Value = "강의명";
                worksheet.Cell(1, 5).Value = "이름";
                worksheet.Cell(1, 6).Value = "정산시수";
                worksheet.Cell(1, 7).Value = "소득구분";
                worksheet.Cell(1, 8).Value = "실지급액";
                worksheet.Cell(1, 9).Value = "송금요청액";


                for (int i = 0; i < result.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = result[i].addfare_date.Year.ToString() + '-' + result[i].addfare_seq.ToString();
                    worksheet.Cell(i + 2, 2).Value = result[i].addfare_date.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 3).Value = result[i].original_company_nm;
                    worksheet.Cell(i + 2, 4).Value = result[i].@class;
                    worksheet.Cell(i + 2, 5).Value = result[i].name;
                    worksheet.Cell(i + 2, 6).Value = result[i].hour;
                    worksheet.Cell(i + 2, 7).Value = result[i].income_type_nm;

                    var all = (float)result[i].hour_price * result[i].hour;
                    var all_tax = Math.Truncate(all * result[i].income / 10) * 10;
                    var employee_all = (float)result[i].contract_price * result[i].hour;
                    var employee_tax = Math.Truncate(employee_all * result[i].income / 10) * 10;
                    var employee_deposit = employee_all - employee_tax;
                    var remittance = all - all_tax - employee_deposit;

                    worksheet.Cell(i + 2, 8).Value = employee_deposit;
                    worksheet.Cell(i + 2, 9).Value = remittance;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

        [Authorize]
        [HttpGet("family/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> GetFamily(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _incamAddfare.SelectFamily(dataTableInputDto, int.Parse(User.Identity.Name));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{addfare_seq:int}")]
        public async Task<ActionResult<IncamAddfareModel>> Get(int addfare_seq)
        {
            return await _incamAddfare.Select(addfare_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IncamAddfareModel incamAddfare)
        {
            incamAddfare.use_yn = 1;
            incamAddfare.reg_dt = DateTime.Now;
            incamAddfare.reg_user = int.Parse(User.Identity.Name);
            incamAddfare.upd_dt = DateTime.Now;
            incamAddfare.upd_user = int.Parse(User.Identity.Name);
            await _incamAddfare.Add(incamAddfare);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IncamAddfareModel incamAddfare)
        {
            incamAddfare.upd_dt = DateTime.Now;
            incamAddfare.upd_user = int.Parse(User.Identity.Name);
            await _incamAddfare.Update(incamAddfare);
            return Ok();
        }


        [Authorize(Roles = "admin")]
        [HttpPut("deposit/{addfare_seq}")]
        public async Task<ActionResult> depositPut(int addfare_seq)
        {
            IncamAddfareModel incamAddfare = new IncamAddfareModel();
            incamAddfare.addfare_seq = addfare_seq;
            incamAddfare.check_yn = 1;
            await _incamAddfare.UpdateDeposit(incamAddfare);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{addfare_seq}")]
        public async Task<ActionResult> Delete(string addfare_seq)
        {
            IncamAddfareModel incamAddfare = new IncamAddfareModel
            {
                addfare_seq = Convert.ToInt32(addfare_seq),
                use_yn = 0
            };

            await _incamAddfare.Delete(incamAddfare);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("SendAddfare")]
        public async Task<ActionResult> SendAddfare([FromBody] List<IncamAddfareModel> incamAddfares)
        {
            await _incamAddfare.SendAddfare(incamAddfares);
            return Ok();
        }
    }
}
