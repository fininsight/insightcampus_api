using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumgroupController : ControllerBase
    {
        private readonly CurriculumgroupInterface _curriculumgroup;

        public CurriculumgroupController(CurriculumgroupInterface curriculumgroup)
        {
            _curriculumgroup = curriculumgroup;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("{class_seq}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int class_seq, int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;
            dataTableInputDto.class_seq = class_seq;

            return await _curriculumgroup.Select(dataTableInputDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CurriculumgroupModel curriculumgroup)
        {
            await _curriculumgroup.Add(curriculumgroup);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CurriculumgroupModel curriculumgroup)
        {
            await _curriculumgroup.Update(curriculumgroup);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{curriculumgroup_seq}")]
        public async Task<ActionResult> Delete(string curriculumgroup_seq)
        {
            CurriculumgroupModel curriculumgroup = new CurriculumgroupModel
            {
                curriculumgroup_seq = Convert.ToInt32(curriculumgroup_seq)
            };

            await _curriculumgroup.Delete(curriculumgroup);
            return Ok();
        }
    }
}
