using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncamContractController : ControllerBase
    {
        private readonly IncamContractInterface _incamContract;

        public IncamContractController(IncamContractInterface incamContract)
        {
            _incamContract = incamContract;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get([FromQuery(Name = "f")] string f, int size, int pageNumber)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _incamContract.Select(dataTableInputDto, filters);
        }

        [HttpGet("excel")]
        public async Task<IActionResult> getExcel([FromQuery(Name = "f")] string f)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            var result = await _incamContract.SelectExcel(filters);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "내부정산.xlsx";

     
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell(1, 1).Value = "계약서 No.";
                worksheet.Cell(1, 2).Value = "강사명";
                worksheet.Cell(1, 3).Value = "원청사";
                worksheet.Cell(1, 4).Value = "강의명";
                worksheet.Cell(1, 5).Value = "시간당금액";
                worksheet.Cell(1, 6).Value = "시간당계약금액";
                worksheet.Cell(1, 7).Value = "시간당인센티브";
                worksheet.Cell(1, 8).Value = "계약기간";


                for (int i = 0; i < result.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = result[i].contract_start_date.Year.ToString() + '-' + result[i].contract_seq.ToString();
                    worksheet.Cell(i + 2, 2).Value = result[i].name;
                    worksheet.Cell(i + 2, 3).Value = result[i].original_company_nm;
                    worksheet.Cell(i + 2, 4).Value = result[i].@class;
                    worksheet.Cell(i + 2, 5).Value = result[i].hour_price;
                    worksheet.Cell(i + 2, 6).Value = result[i].contract_price;
                    worksheet.Cell(i + 2, 7).Value = result[i].hour_incen;
                    worksheet.Cell(i + 2, 8).Value = result[i].contract_start_date.ToString("yyyy-MM-dd") + '~' + result[i].contract_end_date.ToString("yyyy-MM-dd");
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

        [Authorize(Roles = "admin")]
        [HttpGet("{contract_seq}")]
        public async Task<ActionResult<IncamContractModel>> Get(int contract_seq)
        {
            return await _incamContract.Select(contract_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<IncamContractModel>>> SelectContract(string searchText)
        {
            return await _incamContract.SelectContract(searchText);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IncamContractModel incamContract)
        {
            incamContract.use_yn = 1;
            incamContract.reg_dt = DateTime.Now;
            incamContract.reg_user = int.Parse(User.Identity.Name);
            incamContract.upd_dt = DateTime.Now;
            incamContract.upd_user = int.Parse(User.Identity.Name);
            await _incamContract.Add(incamContract);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IncamContractModel incamContract)
        {
            incamContract.upd_dt = DateTime.Now;
            incamContract.upd_user = int.Parse(User.Identity.Name);
            await _incamContract.Update(incamContract);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{contract_seq}")]
        public async Task<ActionResult> Delete(int contract_seq)
        {
            IncamContractModel incamContract = new IncamContractModel
            {
                contract_seq = contract_seq,
                use_yn = 0
            };

            await _incamContract.Delete(incamContract);
            return Ok();
        }
    }
}
