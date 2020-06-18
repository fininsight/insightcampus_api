using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class CurriculumRepository : CurriculumInterface
    {
        private readonly DataContext _context;

        public CurriculumRepository(DataContext context)
        {
            _context = context;
        }

        
        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task Update(CurriculumModel curriculumModel)
        {
            _context.Entry(curriculumModel).Property(x => x.curriculum_nm).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.class_seq).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.duration).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.level).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.parent_seq).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.order).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.video_url).IsModified = true;
            _context.Entry(curriculumModel).Property(x => x.video_type).IsModified = true;
            await _context.SaveChangesAsync();
        }
       
        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from curriculum in _context.CurriculumContext
                    where curriculum.class_seq == dataTableInputDto.class_seq
                    select curriculum);

            result = result.OrderByDescending(o => o.curriculum_seq);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        
        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
