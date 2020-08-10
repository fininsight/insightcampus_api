using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class IncamAddfareRepository : IncamAddfareInterface
    {
        private readonly DataContext _context;

        public IncamAddfareRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IncamAddfareModel incamAddfareModel)
        {
            _context.Entry(incamAddfareModel).Property(x => x.addfare_date).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.teacher_seq).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.contract_seq).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.hour).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.income_type).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from addfare in _context.IncamAddfareContext
                    join contract in _context.IncamContractContext
                      on addfare.contract_seq equals contract.contract_seq
                    join teacher in _context.TeacherContext
                      on addfare.teacher_seq equals teacher.teacher_seq
                    join company in _context.CodeContext
                      on contract.original_company equals company.code_id
                    where company.codegroup_id == "cooperative"
                   select new IncamAddfareModel
                    {
                        addfare_seq = addfare.addfare_seq,
                        teacher_seq = addfare.teacher_seq,
                        contract_seq = addfare.contract_seq,
                        hour = addfare.hour,
                        addfare_date = addfare.addfare_date,
                        income_type = addfare.income_type,
                        original_company_nm = company.code_nm,
                        @class = contract.@class,
                        name = teacher.name
                    });

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<IncamAddfareModel> Select(int addfare_seq)
        {
            var result = await (
                      from incam_addfare in _context.IncamAddfareContext
                      where incam_addfare.addfare_seq == addfare_seq
                      select incam_addfare).SingleAsync();

            return result;
        }

        public async Task<DataTableOutDto> SelectFamily(DataTableInputDto dataTableInputDto, int teacher_seq)
        {
            var result = (
                    from incam_addfare in _context.IncamAddfareContext
                   where incam_addfare.teacher_seq == teacher_seq
                  select incam_addfare);
            result = result.OrderByDescending(o => o.addfare_date);

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
