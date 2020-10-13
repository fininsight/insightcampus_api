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
        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _teacher.Select(dataTableInputDto);
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
            await _teacher.Add(teacher);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TeacherModel teacher)
        {
            await _teacher.UpdateLog(teacher);
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

            await _teacher.DeleteLog(teacher_seq);
            await _teacher.Delete(teacher);
            return Ok();
        }
    }
}
