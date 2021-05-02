using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class ClassStudentRepository : ClassStudentInterface
    {

        private readonly DataContext _context;

        public ClassStudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<DataTableOutDto> Select(int class_seq, DataTableInputDto dataTableInputDto)
        {
            var result = (
                      from cls in _context.ClassContext
                      join order_item in _context.OrderItemContext on class_seq equals order_item.class_seq
                      join order in _context.OrderContext on order_item.order_id equals order.order_id
                      where cls.class_seq == class_seq
                      select new {
                          order_id = order.order_id,
                          order_user_seq = order.order_user_seq,
                          order_date = order.order_date,
                          order_type = order.order_type,
                          order_price = order.order_price,
                          address = order.address,
                      });

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

    }
}
