using System;
using insightcampus_api.Data;
using insightcampus_api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class PdfRepository : PdfInterface
    {
        private readonly DataContext _context;

        public PdfRepository(DataContext context)
        {
            _context = context;
        }

        public Task<IncamAddfareModel> Select(int addfare_seq)
        {
            var result = (
                    from incam_addfare in _context.IncamAddfareContext
                    join contract in _context.IncamContractContext
                    on incam_addfare.contract_seq equals contract.contract_seq
                    join teacher in _context.TeacherContext
                    on contract.teacher_seq equals teacher.teacher_seq
                    join incom in _context.CodeContext
                    on incam_addfare.income_type equals incom.code_id
                    where incam_addfare.addfare_seq == addfare_seq && incom.codegroup_id == "incom"
                    //select incam_addfare).SingleAsync();
                    select new IncamAddfareModel
                    {
                        addfare_seq = incam_addfare.addfare_seq,
                        contract_seq = incam_addfare.contract_seq,
                        hour = incam_addfare.hour,
                        addfare_date = incam_addfare.addfare_date,
                        income_type = incam_addfare.income_type,
                        // original_company_nm = incam_contract.original_company,
                        income_type_nm = incom.code_nm,
                        @class = contract.@class,
                        hour_price = contract.hour_price,
                        hour_incen = contract.hour_incen,
                        contract_price = contract.contract_price,
                        name = teacher.name,
                        rate = float.Parse(incom.value1),
                        income = incam_addfare.income,
                        addfare_gubun = incam_addfare.addfare_gubun
                    }).SingleAsync();



            return result;
        }

        public Task<ClassStudentModel> SelectStudent(int class_seq, int order_user_seq)
        {
            var result = (
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
                          class_nm = cls.class_nm,
                          order_item_seq = order_item.order_item_seq,
                          order_date = order.order_date,
                          start_date = cls.start_date,
                          end_date = cls.end_date,
                          order_type = order.order_type,
                          order_price = order.order_price,
                          address = order.address,
                      }).SingleAsync();

            return result;
        }
    }
}
