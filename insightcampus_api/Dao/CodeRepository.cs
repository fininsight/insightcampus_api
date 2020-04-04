using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using insightcampus_api.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace insightcampus_api.Dao
{
    public class CodeRepository : CodeInterface
    {

        private readonly DataContext _context;

        public CodeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CodeModel codeModel)
        {
            _context.Entry(codeModel).Property(x => x.upd_dt).IsModified = true;
            _context.Entry(codeModel).Property(x => x.upd_user).IsModified = true;
            _context.Entry(codeModel).Property(x => x.code_nm).IsModified = true;
            _context.Entry(codeModel).Property(x => x.value1).IsModified = true;
            _context.Entry(codeModel).Property(x => x.value2).IsModified = true;
            _context.Entry(codeModel).Property(x => x.value3).IsModified = true;
            _context.Entry(codeModel).Property(x => x.value4).IsModified = true;
            _context.Entry(codeModel).Property(x => x.value5).IsModified = true;
            _context.Entry(codeModel).Property(x => x.order_num).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from code in _context.CodeContext
                   where code.codegroup_id == dataTableInputDto.codegroup_id
                  select code);

            result = result.OrderBy(o => o.order_num);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }
    }
}
