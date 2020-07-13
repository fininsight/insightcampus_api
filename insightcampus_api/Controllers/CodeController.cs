using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize(Roles = "admin")]
        [HttpGet("{codegroup_id}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(String codegroup_id, int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;
            dataTableInputDto.codegroup_id = codegroup_id;

            return await _code.Select(dataTableInputDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CodeModel code)
        {            
            code.reg_user = int.Parse(User.Identity.Name);
            code.reg_dt = DateTime.Now;
            code.upd_dt = DateTime.Now;
            await _code.Add(code);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CodeModel code)
        {
            code.upd_user = int.Parse(User.Identity.Name);            
            code.upd_dt = DateTime.Now;
            await _code.Update(code);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{codegroup_id}/{code_id}")]
        public async Task<ActionResult> Delete(string codegroup_id, string code_id)
        {
            CodeModel code = new CodeModel
            {
                codegroup_id = codegroup_id,
                code_id = code_id
            };
            await _code.Delete(code);
            return Ok();
        }
    }
}
