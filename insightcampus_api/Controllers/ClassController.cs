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
    public class ClassController : ControllerBase
    {
        private readonly ClassInterface _class;

        public ClassController(ClassInterface __class)
        {
            _class = __class;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _class.Select(dataTableInputDto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{class_seq}")]
        public async Task<ActionResult<ClassModel>> Get(int class_seq)
        {
            return await _class.Select(class_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClassModel classes)
        {
            await _class.Add(classes);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{seq}")]
        public async Task<ActionResult> Put(int seq, [FromBody] ClassModel classes)
        {
            classes.class_seq = seq;
            await _class.Update(classes);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            ClassModel classes = new ClassModel
            {
                class_seq = seq
            };
            await _class.Delete(classes);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("template")]
        public async Task<ActionResult> TemplateUpdate([FromBody]ClassModel classModel)
        {
            await _class.UpdateTemplate(classModel);
            return Ok();
        }
    }
}
