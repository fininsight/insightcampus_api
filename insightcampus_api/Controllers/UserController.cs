using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserInterface _user;
        private readonly EmailInterface _email;

        public UserController(UserInterface user, EmailInterface email)
        {
            _user = user;
            _email = email;
        }

        [HttpGet("{size}/{pageNumber}/{search?}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber, int search)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _user.Select(dataTableInputDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserModel user)
        {
            await _user.Add(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserModel user)
        {
            user.user_seq = id;
            await _user.Update(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            UserModel user = new UserModel
            {
                user_seq = id
            };
            await _user.Delete(user);
            return Ok();
        }

        [HttpGet("email")]
        public async Task<ActionResult> email()
        {
            _email.SendEmail();
            return Ok();
        }

    }
}
