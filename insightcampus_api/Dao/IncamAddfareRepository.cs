using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class IncamAddfareRepository : IncamAddfareInterface
    {
        private readonly DataContext _context;

        public IncamAddfareRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IncamAddfareModel incamAddfareModel)
        {
            _context.Entry(incamAddfareModel).Property(x => x.addfare_date).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.original_company).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.@class).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.gubun).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.name).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.price).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.hour).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.tax).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.income_type).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.remit).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from incam_addfare in _context.IncamAddfareContext
                    select incam_addfare);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<IncamAddfareModel> Select(int addfare_seq)
        {
            var result = await (
                      from incam_addfare in _context.IncamAddfareContext
                      where incam_addfare.addfare_seq == addfare_seq
                      select incam_addfare).SingleAsync();

            return result;
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
