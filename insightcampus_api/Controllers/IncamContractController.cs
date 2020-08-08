using System;
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
    public class IncamContractController : ControllerBase
    {
        private readonly IncamContractInterface _incamContract;

        public IncamContractController(IncamContractInterface incamContract)
        {
            _incamContract = incamContract;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _incamContract.Select(dataTableInputDto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{contract_seq}")]
        public async Task<ActionResult<IncamContractModel>> Get(int contract_seq)
        {
            return await _incamContract.Select(contract_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IncamContractModel incamContract)
        {
            await _incamContract.Add(incamContract);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IncamContractModel incamContract)
        {
            await _incamContract.Update(incamContract);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{addfare_seq}")]
        public async Task<ActionResult> Delete(string contract_seq)
        {
            IncamContractModel incamAddfare = new IncamContractModel
            {
                contract_seq = Convert.ToInt32(contract_seq)
            };

            await _incamContract.Delete(incamAddfare);
            return Ok();
        }
    }
}
