using System;
using System.Collections;
using System.Collections.Generic;
using insightcampus_api.Model;

namespace insightcampus_api.Data
{
    public class OrderDataDto
    {
        public OrderModel order { get; set; }
        public List<OrderItemModel> orderItem { get; set; }
    }
}
