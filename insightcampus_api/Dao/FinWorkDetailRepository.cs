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
    public class FinWorkDetailRepository : FinWorkDetailInterface
    {
        private readonly DataContext _context;

        public FinWorkDetailRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FinWorkDetailModel finWorkDetailModel)
        {
            _context.Entry(finWorkDetailModel).Property(x => x.year).IsModified = true;
            _context.Entry(finWorkDetailModel).Property(x => x.month).IsModified = true;
            _context.Entry(finWorkDetailModel).Property(x => x.expected_sales).IsModified = true;
            _context.Entry(finWorkDetailModel).Property(x => x.expected_purchase).IsModified = true;
            _context.Entry(finWorkDetailModel).Property(x => x.sales).IsModified = true;
            _context.Entry(finWorkDetailModel).Property(x => x.purchase).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from fin_work_detail in _context.FinWorkDetailContext
                    select fin_work_detail);
            

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<FinWorkDetailModel> Select(int work_detail_seq)
        {
            var result = await (
                      from fin_work_detail in _context.FinWorkDetailContext
                      where fin_work_detail.work_detail_seq == work_detail_seq
                      select fin_work_detail).SingleAsync();

            return result;
        }

        public async Task Delete(FinWorkDetailModel finWorkDetailModel)
        {
            _context.Remove(finWorkDetailModel);
            await _context.SaveChangesAsync();
        }
    }
}