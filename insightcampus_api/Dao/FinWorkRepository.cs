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
    public class FinWorkRepository : FinWorkInterface
    {
        private readonly DataContext _context;

        public FinWorkRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FinWorkModel finWorkModel)
        {
            _context.Entry(finWorkModel).Property(x => x.work_name).IsModified = true;
            _context.Entry(finWorkModel).Property(x => x.start_date).IsModified = true;
            _context.Entry(finWorkModel).Property(x => x.end_date).IsModified = true;
            _context.Entry(finWorkModel).Property(x => x.expected_sales).IsModified = true;
            _context.Entry(finWorkModel).Property(x => x.expected_purchase).IsModified = true;
            _context.Entry(finWorkModel).Property(x => x.sales).IsModified = true;
            _context.Entry(finWorkModel).Property(x => x.purchase).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from fin_work in _context.FinWorkContext
                    select fin_work);
            

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<FinWorkModel> Select(int work_seq)
        {
            var result = await (
                      from fin_work in _context.FinWorkContext
                      where fin_work.work_seq == work_seq
                      select fin_work).SingleAsync();

            return result;
        }

        public async Task<List<FinWorkModel>> SelectFinWork(String searchText)
        {
            var result = (
                    from fin_work in _context.FinWorkContext
                    select fin_work);

            if (searchText != "ALL")
            {
                result = result.Where(t => t.work_name.Contains(searchText));
            }

            return await result.ToListAsync();
        }

        public async Task Delete(FinWorkModel finWorkModel)
        {
            _context.Remove(finWorkModel);
            await _context.SaveChangesAsync();
        }
    }
}