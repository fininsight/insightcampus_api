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
    public class CurriculumController : ControllerBase
    {
        private readonly CurriculumInterface _curriculum;

        public CurriculumController(CurriculumInterface curriculum)
        {
            _curriculum = curriculum;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{class_seq}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int class_seq, int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;
            dataTableInputDto.class_seq = class_seq;

            return await _curriculum.Select(dataTableInputDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CurriculumModel curriculum)
        {
            await _curriculum.Add(curriculum);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CurriculumModel curriculum)
        {
            await _curriculum.Update(curriculum);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{curriculum_seq}")]
        public async Task<ActionResult> Delete(string curriculum_seq)
        {
            CurriculumModel curriculum = new CurriculumModel
            {
                curriculum_seq = Convert.ToInt32(curriculum_seq)
            };

            await _curriculum.Delete(curriculum);
            return Ok();
        }
    }
}
