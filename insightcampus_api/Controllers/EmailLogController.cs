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
    public class EmailLogController : ControllerBase
    {
        private readonly EmailLogInterface _emaillog;

        public EmailLogController(EmailLogInterface __emaillog)
        {
            _emaillog = __emaillog;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _emaillog.Select(dataTableInputDto);
        }

        [HttpGet("{class_seq}")]
        public async Task<ActionResult<EmailLogModel>> Get(int email_log_seq)
        {
            return await _emaillog.Select(email_log_seq);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmailLogModel emaillogs)
        {
            emaillogs.reg_date = DateTime.Now;
            await _emaillog.Add(emaillogs);
            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{email_log_seq}")]
        public async Task<ActionResult> Put([FromBody] EmailLogModel emaillogs, int email_log_seq)
        {
            emaillogs.email_log_seq = email_log_seq;
            await _emaillog.Update(emaillogs);
            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            EmailLogModel emaillogs = new EmailLogModel
            {
                email_log_seq = seq
            };
            emaillogs.use_yn = 0;
            await _emaillog.Delete(emaillogs);
            return Ok();
        }

    }
}
