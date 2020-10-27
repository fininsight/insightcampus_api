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
    public class CouponController : ControllerBase
    {
        private readonly CouponInterface _coupon;

        public CouponController(CouponInterface coupon)
        {
            _coupon = coupon;
        }

        [HttpGet("{size}/{pageNumber}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _coupon.Select(dataTableInputDto);
        }

        [HttpGet("{coupon_seq}")]
        public async Task<ActionResult<CouponModel>> Get(int coupon_seq)
        {
            return await _coupon.Select(coupon_seq);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<CouponModel>>> SelectCoupon(string searchText)
        {
            return await _coupon.SelectCoupon(searchText);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CouponModel coupon)
        {
            coupon.reg_dt = DateTime.Now;
            coupon.reg_user = User.Identity.Name;
            coupon.upd_dt = DateTime.Now;
            coupon.upd_user = User.Identity.Name;
            coupon.use_yn = 1;
            await _coupon.Add(coupon);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CouponModel coupon)
        {
            coupon.upd_dt = DateTime.Now;
            coupon.upd_user = User.Identity.Name;
            await _coupon.Update(coupon);
            return Ok();
        }

        [HttpDelete("{coupon_seq}")]
        public async Task<ActionResult> Delete(string coupon_seq)
        {
            CouponModel coupon = new CouponModel
            {
                coupon_seq = Convert.ToInt32(coupon_seq)
            };

            coupon.upd_dt = DateTime.Now;
            coupon.upd_user = User.Identity.Name;
            coupon.use_yn = 0;
            await _coupon.Delete(coupon);
            return Ok();
        }
    }
}
