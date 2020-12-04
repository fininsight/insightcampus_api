using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;
using insightcampus_api.Data;
using Microsoft.EntityFrameworkCore;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public class EmployProofRepository : EmployProofInterface
    {
        private readonly DataContext _context;

        public EmployProofRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(EmployProofModel employproofModel)
        {
            _context.Entry(employproofModel).Property(x => x.name).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task Update(EmployProofModel employproofModel)
        {
            _context.Entry(employproofModel).Property(x => x.name).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.register_number).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.home_address).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.phone_number).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.department).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.work_address).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.position).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.work_period).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.purpose).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.reg_date).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.upd_date).IsModified = true;
            _context.Entry(employproofModel).Property(x => x.download_count).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                      from emaillog in _context.EmailLogContext
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

        public async Task<EmployProofModel> Select(int employee_proof_seq)
        {
            var result = await (
                      from incam_employee_proof in _context.EmployProofContext
                      where incam_employee_proof.employee_proof_seq == employee_proof_seq
                      select incam_employee_proof).SingleAsync();

            return result;
        }


    }
}