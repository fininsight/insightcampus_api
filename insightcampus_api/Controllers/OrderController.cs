using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderInterface _order;

        public OrderController(OrderInterface order)
        {
            _order = order;
        }

        [HttpGet("{order_id}")]
        public async Task<ActionResult<OrderModel>> Get(int order_id)
        {
            return await _order.Select(order_id);
        }

        [HttpGet("{size:int}/{pageNumber:int}")]
        public async Task<ActionResult<DataTableOutDto>> Get(int size, int pageNumber)
        {
            List<Filter> filters = new List<Filter>();

            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _order.Select(dataTableInputDto, filters);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderModel orders)
        {

            orders.reg_date = DateTime.Now;
            orders.upd_date = DateTime.Now;
            await _order.Add(orders);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderModel orders)
        {
            orders.upd_user = int.Parse(User.Identity.Name);
            orders.upd_date = DateTime.Now;
            orders.order_id = id;
            await _order.Update(orders);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            OrderModel orders = new OrderModel
            {
                order_id = id
            };
            await _order.Delete(orders);
            return Ok();
        }
    }
}
