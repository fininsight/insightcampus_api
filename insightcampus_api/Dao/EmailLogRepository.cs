using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class EmailLogRepository : EmailLogInterface
    {
        private readonly DataContext _context;

        public EmailLogRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(EmailLogModel emaillogModel)
        {
            _context.Entry(emaillogModel).Property(x => x.use_yn).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(EmailLogModel emaillogModel)
        {
            //_context.Entry(emaillogModel).Property(x => x.email_log_seq).IsModified = true;
            _context.Entry(emaillogModel).Property(x => x.subject).IsModified = true;
            _context.Entry(emaillogModel).Property(x => x.contents).IsModified = true;
            _context.Entry(emaillogModel).Property(x => x.to).IsModified = true;
            _context.Entry(emaillogModel).Property(x => x.reg_date).IsModified = true;
            //_context.Entry(emaillogModel).Property(x => x.use_yn).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                      from emaillog in _context.EmailLogContext
                      where emaillog.use_yn == 1
                      select emaillog);

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

        public async Task<EmailLogModel> Select(int email_log_seq)
        {
            var result = await (
                      from emaillog in _context.EmailLogContext
                      where emaillog.email_log_seq == email_log_seq
                      select emaillog).SingleAsync();

            return result;
        }

    }
}
