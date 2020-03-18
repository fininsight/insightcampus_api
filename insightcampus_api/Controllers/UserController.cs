using System;
using System.Collections.Generic;
using insightcampus_api.Dao;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserInterface _user;

        public UserController(UserInterface user)
        {
            _user = user;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            return _user.Select();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
