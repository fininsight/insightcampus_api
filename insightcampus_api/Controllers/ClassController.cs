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
    public class ClassController : ControllerBase
    {
        private readonly ClassInterface _class;

        public ClassController(ClassInterface __class)
        {
            _class = __class;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _class.Select(dataTableInputDto);
        }

        [HttpGet("{class_seq}")]
        public async Task<ActionResult<ClassModel>> Get(int class_seq)
        {
            return await _class.Select(class_seq);
        }

        [HttpPut("template")]
        public async Task<ActionResult> TemplateUpdate([FromBody]ClassModel classModel)
        {
            await _class.UpdateTemplate(classModel);
            return Ok();
        }
    }
}
