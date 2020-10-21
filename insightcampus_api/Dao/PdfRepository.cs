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
                    from incam_addfare in _context.PdfContext
                    join contract in _context.IncamContractContext
                    on incam_addfare.contract_seq equals contract.contract_seq
                    join teacher in _context.TeacherContext
                    on contract.teacher_seq equals teacher.teacher_seq
                    join incom in _context.CodeContext
                    on incam_addfare.income_type equals incom.code_id
                    where incam_addfare.addfare_seq == addfare_seq
                    //select incam_addfare).SingleAsync();
                    select new IncamAddfareModel
                    {
                        addfare_seq = incam_addfare.addfare_seq,
                        contract_seq = incam_addfare.contract_seq,
                        hour = incam_addfare.hour,
                        addfare_date = incam_addfare.addfare_date,
                        income_type = incam_addfare.income_type,
                        original_company_nm = incam_contract.original_company,
                        income_type_nm = incom.code_nm,
                        @class = contract.@class,
                        hour_price = contract.hour_price,
                        hour_incen = contract.hour_incen,
                        contract_price = contract.contract_price,
                        name = teacher.name,
                        rate = float.Parse(incom.value1)
                    }).SingleAsync();



            return result;
        }

    }
}
