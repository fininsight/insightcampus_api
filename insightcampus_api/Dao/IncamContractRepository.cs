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
            _context.Entry(incamContractModel).Property(x => x.teacher_seq).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.hour_incen).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.contract_price).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.contract_start_date).IsModified = true;
            _context.Entry(incamContractModel).Property(x => x.contract_end_date).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from contract in _context.IncamContractContext
                    join company in _context.CodeContext
                      on contract.original_company equals company.code_id
                    join teacher in _context.TeacherContext
                      on contract.teacher_seq equals teacher.teacher_seq
                   where company.codegroup_id == "cooperative"
                 orderby contract.contract_seq descending
                  select new IncamContractModel
                  {
                      contract_seq = contract.contract_seq,
                      teacher_seq = contract.teacher_seq,
                      @class = contract.@class,
                      original_company = contract.original_company,
                      original_company_nm = company.code_nm,
                      hour_price = contract.hour_price,
                      hour_incen = contract.hour_incen,
                      contract_price = contract.contract_price,
                      contract_start_date = contract.contract_start_date,
                      contract_end_date = contract.contract_end_date,
                      name = teacher.name
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
            var result = (
                    from contract in _context.IncamContractContext
                    join company in _context.CodeContext
                      on contract.original_company equals company.code_id
                    join teacher in _context.TeacherContext
                      on contract.teacher_seq equals teacher.teacher_seq
                   where company.codegroup_id == "cooperative"
                 orderby contract.contract_seq descending
                  select new IncamContractModel
                    {
                        contract_seq = contract.contract_seq,
                        teacher_seq = contract.teacher_seq,
                        @class = contract.@class,
                        name = teacher.name,
                        original_company = contract.original_company,
                        original_company_nm = company.code_nm,
                        hour_price = contract.hour_price,
                        hour_incen = contract.hour_incen,
                        contract_price = contract.contract_price,
                        contract_start_date = contract.contract_start_date,
                        contract_end_date = contract.contract_end_date});

            if (searchText != "ALL")
            {
                result = result.Where(t => t.name.Contains(searchText) || t.original_company_nm.Contains(searchText) || t.@class.Contains(searchText));
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
