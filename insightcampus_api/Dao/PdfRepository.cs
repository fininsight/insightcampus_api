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
                    where incam_addfare.addfare_seq == addfare_seq
                    select incam_addfare).SingleAsync();


            return result;
        }

    }
}
