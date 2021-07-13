using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace insightcampus_api.Dao
{
    public class OrderRepository : OrderInterface
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(OrderModel orderModel)
        {
            _context.Entry(orderModel).Property(x => x.order_date).IsModified = true;
            _context.Entry(orderModel).Property(x => x.order_price).IsModified = true;
            _context.Entry(orderModel).Property(x => x.order_type).IsModified = true;
            _context.Entry(orderModel).Property(x => x.order_user_seq).IsModified = true;
            _context.Entry(orderModel).Property(x => x.address).IsModified = true;
            _context.Entry(orderModel).Property(x => x.upd_date).IsModified = true;
            _context.Entry(orderModel).Property(x => x.upd_user).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters)
        {
            var result = (
                      from ord in _context.OrderContext
                      select ord);

            foreach (var filter in filters)
            {
                if (filter.k == "order_type")
                {
                    result = result.Where(w => w.order_type.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "address")
                {
                    result = result.Where(w => w.address.Contains(filter.v.Replace(" ", "")));
                }
            }

            result = result.OrderByDescending(o => o.order_id);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }


        public async Task<OrderModel> Select(int order_id)
        {
            var result = await (
                      from ord in _context.OrderContext
                      where ord.order_id == order_id
                      select ord).SingleAsync();

            return result;
        }
    }
}
