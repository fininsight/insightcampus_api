using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace insightcampus_api.Dao
{
    public class OrderItemRepository : OrderItemInterface
    {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddList(List<OrderItemModel> orderItemList)
        {

            var max_id = await (
                      from ord in _context.OrderContext
                      select ord.order_id).MaxAsync();

            foreach (var orderItem in orderItemList)
            {
                OrderItemModel ordItem = new OrderItemModel()
                {
                    order_id = max_id,
                    class_seq = orderItem.class_seq,
                    price = orderItem.price
                };
                await _context.AddAsync(ordItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddList(List<OrderItemModel> orderItemList, int order_id)
        {
            foreach (var orderItem in orderItemList)
            {
                OrderItemModel ordItem = new OrderItemModel()
                {
                    order_id = order_id,
                    class_seq = orderItem.class_seq,
                    price = orderItem.price
                };
                await _context.AddAsync(ordItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderItemModel>> SelectItems(int order_id)
        {
            var result = await (
                      from ord in _context.OrderItemContext
                      where ord.order_id == order_id
                      select ord).ToListAsync();
            return result;
        }

        public async Task RemoveAll(int order_id)
        {
            var result = (
                      from ord in _context.OrderItemContext
                      where ord.order_id == order_id
                      select ord);

            _context.RemoveRange(result);
            await _context.SaveChangesAsync();
        }
    }
}
