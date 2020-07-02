using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class ClassNoticeRepository : ClassNoticeInterface
    {
        private readonly DataContext _context;

        public ClassNoticeRepository(DataContext context)
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

        public async Task Update(ClassNoticeModel classNoticeModel)
        {
            _context.Entry(classNoticeModel).Property(x => x.class_seq).IsModified = true;
            _context.Entry(classNoticeModel).Property(x => x.parent_seq).IsModified = true;
            _context.Entry(classNoticeModel).Property(x => x.title).IsModified = true;
            _context.Entry(classNoticeModel).Property(x => x.content).IsModified = true;
            _context.Entry(classNoticeModel).Property(x => x.upd_dt).IsModified = true;
            _context.Entry(classNoticeModel).Property(x => x.upd_user).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from class_notice in _context.ClassNoticeContext
                    select class_notice);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<ClassNoticeModel> Select(int class_notice_seq)
        {
            var result = await (
                      from class_notice in _context.ClassNoticeContext
                      where class_notice.class_notice_seq == class_notice_seq
                      select class_notice).SingleAsync();

            return result;
        }
    }
}
