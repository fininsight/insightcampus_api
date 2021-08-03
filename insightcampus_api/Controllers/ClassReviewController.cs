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
    public class ClassReviewController : ControllerBase
    {
        private readonly ClassReviewInterface _classreview;

        public ClassReviewController(ClassReviewInterface classreview)
        {
            _classreview = classreview;
        }

        [HttpGet("{class_seq}/{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int class_seq, int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;
            dataTableInputDto.class_seq = class_seq;

            return await _classreview.Select(dataTableInputDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClassReviewModel classReview)
        {
            classReview.reg_dt = DateTime.Now;
            classReview.upd_dt = DateTime.Now;
            await _classreview.Add(classReview);
            return Ok();
        }

        [HttpPut("{seq}")]
        public async Task<ActionResult> Put(int seq, [FromBody] ClassReviewModel classReview)
        {
            classReview.class_review_seq = seq;
            await _classreview.Update(classReview);
            return Ok();
        }

        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            ClassReviewModel classReview = new ClassReviewModel
            {
                class_review_seq = seq
            };
            await _classreview.Delete(classReview);
            return Ok();
        }

    }

}