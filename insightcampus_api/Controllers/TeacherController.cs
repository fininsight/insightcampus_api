using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherInterface _teacher;

        public TeacherController(TeacherInterface teacher)
        {
            _teacher = teacher;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{size:int}/{pageNumber:int}")]
        public async Task<ActionResult<DataTableOutDto>> Get([FromQuery(Name = "f")] string f, int size, int pageNumber)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _teacher.Select(dataTableInputDto, filters);
        }

        [HttpGet("excel")]
        public async Task<IActionResult> getExcel([FromQuery(Name = "f")] string f)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            //var result = await _teacher.SelectExcel(filters);
            var result = await _teacher.SelectExcel(filters);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "강사관리.xlsx";


            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell(1, 1).Value = "강사명";
                worksheet.Cell(1, 2).Value = "이메일";
                worksheet.Cell(1, 3).Value = "핸드폰 번호";
                worksheet.Cell(1, 4).Value = "주소";

                for (int i = 0; i < result.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = result[i].name;
                    worksheet.Cell(i + 2, 2).Value = result[i].email;
                    worksheet.Cell(i + 2, 3).Value = result[i].phone;
                    worksheet.Cell(i + 2, 4).Value = result[i].address;
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
        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<TeacherModel>>> SelectTeacher(string searchText)
        {
            return await _teacher.SelectTeacher(searchText);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{teacher_seq}")]
        public async Task<ActionResult<TeacherModel>> Get(int teacher_seq)
        {
            return await _teacher.Select(teacher_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TeacherModel teacher)
        {
            Random rnd = new Random();
            string password = rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();

            teacher.passwd = password;
            await _teacher.Add(teacher);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TeacherModel teacher)
        {
            Random rnd = new Random();
            string password = rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();
            password += rnd.Next(0, 9).ToString();

            teacher.passwd = password;
            await _teacher.UpdateLog(teacher.teacher_seq);
            await _teacher.Update(teacher);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{teacher_seq}")]
        public async Task<ActionResult> Delete(int teacher_seq)
        {
            TeacherModel teacher = new TeacherModel
            {
                teacher_seq = teacher_seq,
                use_yn = 0
            };

            await _teacher.Delete(teacher);
            return Ok();
        }
    }
}
