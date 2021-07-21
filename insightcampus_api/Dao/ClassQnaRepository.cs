using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class ClassQnaRepository : ClassQnaInterface
    {
        private readonly DataContext _context;

        public ClassQnaRepository(DataContext context)
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

        public async Task Update(ClassQnaModel classQnaModel)
        {
            _context.Entry(classQnaModel).Property(x => x.class_seq).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.parent_seq).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.title).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.content).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.reply).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.reg_user).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.reg_dt).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.upd_user).IsModified = true;
            _context.Entry(classQnaModel).Property(x => x.upd_dt).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, int class_seq)
        {
            var result = (
                    from class_qna in _context.ClassQnaContext
                    where class_qna.class_seq == class_seq
                    orderby class_qna.reg_dt descending
                    select class_qna);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<ClassQnaModel> Select(int class_qna_seq)
        {
            var result = await (
                      from class_qna in _context.ClassQnaContext
                      where class_qna.class_qna_seq == class_qna_seq
                      select class_qna).SingleAsync();

            return result;
        }
    }
}