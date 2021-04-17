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
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemInterface _orderItem;

        public OrderItemController(OrderItemInterface orderItem)
        {
            _orderItem = orderItem;
        }

        [HttpGet("{order_id}")]
        public async Task<ActionResult<List<OrderItemModel>>> Get(int order_id)
        {
            return await _orderItem.SelectItems(order_id);
        }
    }
}
