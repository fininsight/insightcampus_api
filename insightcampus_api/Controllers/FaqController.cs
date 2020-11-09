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
    public class FaqController : ControllerBase
    {
        private readonly FaqInterface _faq;

        public FaqController(FaqInterface faq)
        {
            _faq = faq;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _faq.Select(dataTableInputDto);
        }

        [HttpGet("{faq_seq}")]
        public async Task<ActionResult<FaqModel>> Get(int faq_seq)
        {
            return await _faq.Select(faq_seq);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<FaqModel>>> SelectFaq(string searchText)
        {
            return await _faq.SelectFaq(searchText);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FaqModel faq)
        {
            faq.reg_dt = DateTime.Now;
            faq.reg_user = User.Identity.Name;
            faq.upd_dt = DateTime.Now;
            faq.upd_user = User.Identity.Name;
            await _faq.Add(faq);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] FaqModel faq)
        {
            faq.upd_dt = DateTime.Now;
            faq.upd_user = User.Identity.Name;
            await _faq.Update(faq);
            return Ok();
        }

        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            FaqModel faqs = new FaqModel
            {
                faq_seq = seq
            };
            await _faq.Delete(faqs);
            return Ok();
        }
    }
}
