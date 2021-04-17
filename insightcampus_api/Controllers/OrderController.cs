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
        private readonly OrderItemInterface _orderItem;

        public OrderController(OrderInterface order, OrderItemInterface orderItem)
        {
            _order = order;
            _orderItem = orderItem;
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
        public async Task<ActionResult> Post([FromBody] OrderDataDto orders)
        {
            orders.order.reg_date = DateTime.Now;
            orders.order.upd_date = DateTime.Now;
            await _order.Add(orders.order);
            await _orderItem.AddList(orders.orderItem);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{order_id}")]
        public async Task<ActionResult> Put(int order_id, [FromBody] OrderDataDto orders)
        {
            orders.order.upd_user = int.Parse(User.Identity.Name);
            orders.order.upd_date = DateTime.Now;
            orders.order.order_id = order_id;
            await _orderItem.RemoveAll(order_id);
            await _orderItem.AddList(orders.orderItem);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{order_id}")]
        public async Task<ActionResult> Delete(int order_id)
        {
            OrderModel orders = new OrderModel
            {
                order_id = order_id
            };
            await _orderItem.RemoveAll(order_id);
            await _order.Delete(orders);
            return Ok();
        }
    }
}
