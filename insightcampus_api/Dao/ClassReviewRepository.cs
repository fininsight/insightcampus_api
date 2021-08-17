using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class ClassReviewRepository : ClassReviewInterface
    {
        private readonly DataContext _context;

        public ClassReviewRepository(DataContext context)
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
                    from class_review in _context.ClassReviewContext
                    where class_review.class_seq == dataTableInputDto.class_seq
                    select class_review);

            result = result.OrderByDescending(o => o.class_review_seq);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task Update(ClassReviewModel classReviewModel)
        {
            _context.Entry(classReviewModel).Property(x => x.class_seq).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.parent_seq).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.title).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.content).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.reg_user).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.reg_dt).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.upd_user).IsModified = true;
            _context.Entry(classReviewModel).Property(x => x.upd_dt).IsModified = true;

            await _context.SaveChangesAsync();
        }
    }
}