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

        private readonly EmailInterface _email;

        public ClassStudentRepository(DataContext context, EmailInterface email)
        {
            _context = context;
            _email = email;
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

        public async Task SendCertification(int class_seq, int order_user_seq, string file_path)
        {

            var result = await (
                      from cls in _context.ClassContext
                      join order_item in _context.OrderItemContext on class_seq equals order_item.class_seq
                      join order in _context.OrderContext on order_item.order_id equals order.order_id
                      join user in _context.UserContext on order.order_user_seq equals user.user_seq
                      where cls.class_seq == class_seq && order.order_user_seq == order_user_seq
                      select new ClassStudentModel
                      {
                          order_id = order.order_id,
                          order_user_seq = order.order_user_seq,
                          name = user.name,
                          email = user.email,
                          class_nm = cls.class_nm,
                          order_item_seq = order_item.order_item_seq,
                          order_date = order.order_date,
                          start_date = cls.start_date,
                          end_date = cls.end_date,
                          order_type = order.order_type,
                          order_price = order.order_price,
                          address = order.address,
                      }).SingleAsync();

            string title = $"[핀인사이트] {result.name} 님 {result.class_nm} 수료증";

            string content = $"{result.name}님  {result.class_nm} 수료증 보내드립니다.";

            string[] test = { "bill@fininsight.co.kr" };
            await _email.SendEmail(result.email, title, content, file_path, test);

            EmailLogModel emailLog = new EmailLogModel
            {
                use_yn = 1,
                subject = title,
                contents = content,
                to = result.email,
                type = "certification"
            };

            await _context.AddAsync(emailLog);
            await _context.SaveChangesAsync();
        }

    }
}
