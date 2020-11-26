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
    public class CommunityController : ControllerBase
    {
        private readonly CommunityInterface  _community;

        public CommunityController(CommunityInterface community)
        {
            _community = community;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _community.Select(dataTableInputDto);
        }

        [HttpGet("{board_seq}")]
        public async Task<ActionResult<CommunityModel>> Get(int board_seq)
        {
            return await _community.Select(board_seq);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CommunityModel board)
        {
            board.reg_dt = DateTime.Now;
            board.upd_dt = DateTime.Now;
            await _community.Add(board);
            return Ok();
        }

        [HttpPut("{seq}")]
        public async Task<ActionResult> Put(int seq, [FromBody] CommunityModel board)
        {
            board.upd_dt = DateTime.Now;
            board.board_seq = seq;
            await _community.Update(board);
            return Ok();
        }

        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            CommunityModel board = new CommunityModel
            {
                board_seq = seq
            };
            await _community.Delete(board);
            return Ok();
        }

        [HttpPut("template")]
        public async Task<ActionResult> TemplateUpdate([FromBody] CommunityModel communityModel)
        {
            await _community.UpdateTemplate(communityModel);
            return Ok();
        }
    }
}
