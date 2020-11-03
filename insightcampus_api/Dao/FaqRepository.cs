using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace insightcampus_api.Dao
{
    public class FaqRepository : FaqInterface
    {
        private readonly DataContext _context;

        public FaqRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FaqModel faqModel)
        {
            _context.Entry(faqModel).Property(x => x.faq_nm).IsModified = true;
            _context.Entry(faqModel).Property(x => x.content).IsModified = true;
            _context.Entry(faqModel).Property(x => x.upd_dt).IsModified = true;
            _context.Entry(faqModel).Property(x => x.upd_user).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from faq in _context.FaqContext
                    select faq);
            

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<FaqModel> Select(int faq_seq)
        {
            var result = await (
                      from faq in _context.FaqContext
                      where faq.faq_seq == faq_seq
                      select faq).SingleAsync();

            return result;
        }

        public async Task<List<FaqModel>> SelectFaq(String searchText)
        {
            var result = (
                    from faq in _context.FaqContext
                    select faq);

            if (searchText != "ALL")
            {
                result = result.Where(t => t.faq_nm.Contains(searchText));
            }

            return await result.ToListAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}