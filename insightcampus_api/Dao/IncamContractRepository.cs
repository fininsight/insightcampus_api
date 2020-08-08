using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace insightcampus_api.Dao
{
    public class IncamContractRepository: IncamContractInterface
    {
        private readonly DataContext _context;

        public IncamContractRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IncamContractModel incamContractModel)
        {
            _context.Entry(incamContractModel).Property(x => x.@class).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.original_company).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.hour_price).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.hour_incen).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.contract_day).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.addfare_seq).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from incam_contract in _context.IncamContractContext
                  select incam_contract);
            result = result.OrderByDescending(o => o.addfare_seq);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<IncamContractModel> Select(int contract_seq)
        {
            var result = await (
                      from incam_contract in _context.IncamContractContext
                     where incam_contract.contract_seq == contract_seq
                    select incam_contract).SingleAsync();
            return result;
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
