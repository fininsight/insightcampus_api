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
    public class FinWorkController : ControllerBase
    {
        private readonly FinWorkInterface _finWork;

        public FinWorkController(FinWorkInterface finWork)
        {
            _finWork = finWork;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _finWork.Select(dataTableInputDto);
        }

        [HttpGet("{work_seq}")]
        public async Task<ActionResult<FinWorkModel>> Get(int work_seq)
        {
            return await _finWork.Select(work_seq);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<FinWorkModel>>> SelectFinWork(string searchText)
        {
            return await _finWork.SelectFinWork(searchText);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FinWorkModel finWork)
        {
            await _finWork.Add(finWork);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] FinWorkModel finWork)
        {
            await _finWork.Update(finWork);
            return Ok();
        }

        [HttpDelete("{work_seq}")]
        public async Task<ActionResult> Delete(string work_seq)
        {
            FinWorkModel finWork = new FinWorkModel
            {
                work_seq = Convert.ToInt32(work_seq)
            };

            await _finWork.Delete(finWork);
            return Ok();
        }
    }
}
