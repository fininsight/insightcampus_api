using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WPBoardNoticeController : ControllerBase
    {
        private readonly WPBoardNoticeInterface _wpBoard;

        public WPBoardNoticeController(WPBoardNoticeInterface wpBoard)
        {
            _wpBoard = wpBoard;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _wpBoard.Select(dataTableInputDto);
        }

        [HttpGet("library/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Library(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _wpBoard.SelectLibrary(dataTableInputDto);
        }

        [HttpGet("review/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Review(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _wpBoard.SelectReview(dataTableInputDto);
        }

    }
}
