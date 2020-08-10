using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            _context.Entry(incamContractModel).Property(x => x.contract_price).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.contract_start_date).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.contract_end_date).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from incam_contract in _context.IncamContractContext
                    join company in _context.CodeContext
                      on incam_contract.original_company equals company.code_id
                   where company.codegroup_id == "cooperative"
                  select new IncamContractModel
                  {
                      contract_seq = incam_contract.contract_seq,
                      @class = incam_contract.@class,
                      original_company = incam_contract.original_company,
                      original_company_nm = company.code_nm,
                      hour_price = incam_contract.hour_price,
                      hour_incen = incam_contract.hour_price,
                      contract_price = incam_contract.contract_price,
                      contract_start_date = incam_contract.contract_start_date,
                      contract_end_date = incam_contract.contract_end_date,
                  });
            result = result.OrderByDescending(o => o.contract_seq);

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

        public async Task<List<IncamContractModel>> SelectContract(String searchText)
        {
            /*
            var result = (
                    from teacher in _context.IncamContractContext
                    select teacher);
            */
            var result = (
                    from incam_contract in _context.IncamContractContext
                    join company in _context.CodeContext
                      on incam_contract.original_company equals company.code_id
                    where company.codegroup_id == "cooperative"
                    select new IncamContractModel
                    {
                        contract_seq = incam_contract.contract_seq,
                        @class = incam_contract.@class,
                        original_company = incam_contract.original_company,
                        original_company_nm = company.code_nm,
                        hour_price = incam_contract.hour_price,
                        hour_incen = incam_contract.hour_incen,
                        contract_price = incam_contract.contract_price,
                        contract_start_date = incam_contract.contract_start_date,
                        contract_end_date = incam_contract.contract_end_date});

            if (searchText != "ALL")
            {
                result = result.Where(t => (t.original_company_nm + " " + t.@class).Contains(searchText));
            }

            return await result.ToListAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
