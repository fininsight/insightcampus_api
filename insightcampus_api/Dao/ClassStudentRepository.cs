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
                      join user in _context.UserContext on order.order_user_seq equals user.user_seq
                      where cls.class_seq == class_seq
                      select new {
                          order_id = order.order_id,
                          order_user_seq = order.order_user_seq,
                          name = user.name,
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
                          survey_url = cls.survey_url
                      }).SingleAsync();

            string title = $"[인사이트 캠퍼스/ {result.class_nm}] 수료증 및 종강 설문조사 안내드립니다.";
            
            string content = $@"
                <p>안녕하세요,</p> 
                <p>인사이트 캠퍼스입니다.</p>
                <p>강의 수강하시느라 수고 많으셨습니다.</p>
                <br />
                <p>=========================</p>
                <p>- 수료증 (PDF 파일 첨부)</p>
                <br />
                <p>-설문조사 참여</p>
                <p>더 나은 교육을 위해 설문을 진행합니다.</p>
                <p>설문해주신 분들께 할인쿠폰을 드립니다.</p>
                <br />
                <p>*설문 맨 하단에 쿠폰코드가 있습니다.</p>
                <p>*할인쿠폰 종강 후 6개월 내 / 1회 / 타쿠폰과 중복 사용 가능.</p>
                <br />
                <p>설문하러가기>> <a href='{result.survey_url}'>{result.survey_url}</a></p>
                <br />
                <p>=========================</p>
                <br />
                <p>좋은 교육으로 또 뵙겠습니다.</p>
                <p>문의 있으시면 언제든 연락바랍니다.</p>
                <p>감사합니다.</p>";

            string[] sender = { "edu@fins.ai" };

            string fileName = $"{result.class_nm}강의수료증({result.name}님)";

            await _email.SendEmail(result.email, title, content, file_path, fileName, sender);

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
