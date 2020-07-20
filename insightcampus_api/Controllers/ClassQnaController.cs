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
    public class ClassQnaController : ControllerBase
    {
        private readonly ClassQnaInterface _class_qna;

        public ClassQnaController(ClassQnaInterface class_qna)
        {
            _class_qna = class_qna;
        }

        [HttpGet("{class_seq}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int class_seq ,int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _class_qna.Select(dataTableInputDto, class_seq);
        }

        [HttpGet("{class_qna_seq}")]
        public async Task<ActionResult<ClassQnaModel>> Get(int class_qna_seq)
        {
            return await _class_qna.Select(class_qna_seq);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClassQnaModel classqnaes)
        {
            classqnaes.reg_dt = DateTime.Now;
            classqnaes.upd_dt = DateTime.Now;
            await _class_qna.Add(classqnaes);
            return Ok();
        }

        [HttpPut("{seq}")]
        public async Task<ActionResult> Put(int seq, [FromBody] ClassQnaModel classqnaes)
        {
            classqnaes.class_qna_seq = seq;
            await _class_qna.Update(classqnaes);
            return Ok();
        }

        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            ClassQnaModel classqnaes = new ClassQnaModel
            {
                class_qna_seq = seq
            };
            await _class_qna.Delete(classqnaes);
            return Ok();
        }
    }
}
