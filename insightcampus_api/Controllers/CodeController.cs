using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly CodeInterface _code;

        public CodeController(CodeInterface code)
        {
            _code = code;
        }

        [HttpGet("{codegroup_id}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(String codegroup_id, int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;
            dataTableInputDto.codegroup_id = codegroup_id;

            return await _code.Select(dataTableInputDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CodeModel code)
        {
            code.reg_dt = DateTime.Now;
            code.upd_dt = DateTime.Now;
            await _code.Add(code);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CodeModel code)
        {
            await _code.Update(code);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] CodeModel code)
        {
            await _code.Delete(code);
            return Ok();
        }
    }
}
