using System;
using System.Threading.Tasks;
using System.Linq;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;

namespace insightcampus_api.Dao
{
    public class ClassRepository : ClassInterface
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
        {
            _context = context;
        }

        public Task Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Update(ClassModel classModel)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTemplate(ClassModel classModel)
        {
            _context.Entry(classModel).Property(x => x.template).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                      from cls in _context.ClassContext
                    select cls);

            // result = result.OrderByDescending(o => o.reg_dt);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<ClassModel> Select(int class_seq)
        {
            var result = await (
                      from cls in _context.ClassContext
                     where cls.class_seq == class_seq
                    select cls).SingleAsync();

            return result;
        }

    }
}
