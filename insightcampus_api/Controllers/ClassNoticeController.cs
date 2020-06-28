using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassNoticeController : ControllerBase
    {
        private readonly ClassNoticeInterface _class_notice;

        public ClassNoticeController(ClassNoticeInterface class_notice)
        {
            _class_notice = class_notice;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _class_notice.Select(dataTableInputDto);
        }

        [HttpGet("{class_notice_seq}")]
        public async Task<ActionResult<ClassNoticeModel>> Get(int class_notice_seq)
        {
            return await _class_notice.Select(class_notice_seq);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClassNoticeModel classnotices)
        {
            await _class_notice.Add(classnotices);
            return Ok();
        }

        [HttpPut("{seq}")]
        public async Task<ActionResult> Put(int seq, [FromBody] ClassNoticeModel classnotices)
        {
            classnotices.class_notice_seq = seq;
            await _class_notice.Update(classnotices);
            return Ok();
        }

        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            ClassNoticeModel classnotices = new ClassNoticeModel
            {
                class_notice_seq = seq
            };
            await _class_notice.Delete(classnotices);
            return Ok();
        }
    }
}
