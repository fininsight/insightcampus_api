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
    public class IncamAddfareController : ControllerBase
    {
        private readonly IncamAddfareInterface _incamAddfare;

        public IncamAddfareController(IncamAddfareInterface incamAddfare)
        {
            _incamAddfare = incamAddfare;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _incamAddfare.Select(dataTableInputDto);
        }

        [HttpGet("{addfare_seq}")]
        public async Task<ActionResult<IncamAddfareModel>> Get(int addfare_seq)
        {
            return await _incamAddfare.Select(addfare_seq);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IncamAddfareModel incamAddfare)
        {
            await _incamAddfare.Add(incamAddfare);
            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IncamAddfareModel incamAddfare)
        {
            await _incamAddfare.Update(incamAddfare);
            return Ok();
        }


        [HttpDelete("{addfare_seq}")]
        public async Task<ActionResult> Delete(string addfare_seq)
        {
            IncamAddfareModel incamAddfare = new IncamAddfareModel
            {
                addfare_seq = Convert.ToInt32(addfare_seq)
            };

            await _incamAddfare.Delete(incamAddfare);
            return Ok();
        }
    }
}
