using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodegroupController : ControllerBase
    {
        private readonly CodegroupInterface _codegroup;

        public CodegroupController(CodegroupInterface codegroup)
        {
            _codegroup = codegroup;
        }

        [Authorize]
        [HttpGet("{size}/{pageNumber}/{search?}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber, String search)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _codegroup.Select(dataTableInputDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CodegroupModel codegroup)
        {
            codegroup.reg_user = int.Parse(User.Identity.Name);
            codegroup.reg_dt = DateTime.Now;
            codegroup.upd_dt = DateTime.Now;
            await _codegroup.Add(codegroup);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CodegroupModel codegroup)
        {
            codegroup.upd_user = int.Parse(User.Identity.Name);            
            codegroup.upd_dt = DateTime.Now;
            await _codegroup.Update(codegroup);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{codegroup_id}")]
        public async Task<ActionResult> Delete(string codegroup_id)
        {
            CodegroupModel codegroup = new CodegroupModel
            {
                codegroup_id = codegroup_id
            };
            await _codegroup.Delete(codegroup);
            return Ok();
        }
    }
}
