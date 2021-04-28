using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class CurriculumgroupRepository : CurriculumgroupInterface
    {
        private readonly DataContext _context;

        public CurriculumgroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
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
                    from curriculumgroup in _context.CurriculumgroupContext
                    where curriculumgroup.class_seq == dataTableInputDto.class_seq
                    select curriculumgroup);

            result = result.OrderByDescending(o => o.curriculumgroup_seq);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task Update(CurriculumgroupModel curriculumgroupModel)
        {
           // _context.Entry(curriculumgroupModel).Property(x => x.curriculumgroup_seq).IsModified = true;
            _context.Entry(curriculumgroupModel).Property(x => x.curriculumgroup_nm).IsModified = true;
            _context.Entry(curriculumgroupModel).Property(x => x.class_seq).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
