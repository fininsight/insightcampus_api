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
                    join incam_contract in _context.IncamContractContext on incam_addfare.contract_seq equals incam_contract.contract_seq
                    join teacher in _context.TeacherContext on incam_contract.teacher_seq equals teacher.teacher_seq
                    where incam_addfare.addfare_seq == addfare_seq
                    select new IncamAddfareModel
                    {
                        addfare_seq = incam_addfare.addfare_seq,
                        contract_seq = incam_addfare.contract_seq,
                        hour = incam_addfare.hour,
                        addfare_date = incam_addfare.addfare_date,
                        income_type = incam_addfare.income_type,
                        original_company_nm = incam_contract.original_company,
                        @class = incam_contract.@class,
                        name = teacher.name,
                        hour_incen = incam_contract.hour_incen,
                        hour_price = incam_contract.hour_price,
                        contract_price = incam_contract.contract_price
                    }).SingleAsync();
                    //select incam_addfare).SingleAsync();


            return result;
        }

    }
}
