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
    public class FinWorkDetailController : ControllerBase
    {
        private readonly FinWorkDetailInterface _finWorkDetail;

        public FinWorkDetailController(FinWorkDetailInterface finWorkDetail)
        {
            _finWorkDetail = finWorkDetail;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _finWorkDetail.Select(dataTableInputDto);
        }

        [HttpGet("{work_seq}")]
        public async Task<ActionResult<FinWorkDetailModel>> Get(int work_detail_seq)
        {
            return await _finWorkDetail.Select(work_detail_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FinWorkDetailModel finWorkDetail)
        {
            await _finWorkDetail.Add(finWorkDetail);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] FinWorkDetailModel finWorkDetail)
        {
            await _finWorkDetail.Update(finWorkDetail);
            return Ok();
        }

        [HttpDelete("{work_detail_seq}")]
        public async Task<ActionResult> Delete(string work_detail_seq)
        {
            FinWorkDetailModel finWorkDetail = new FinWorkDetailModel
            {
                work_detail_seq = Convert.ToInt32(work_detail_seq)
            };

            await _finWorkDetail.Delete(finWorkDetail);
            return Ok();
        }
    }
}
