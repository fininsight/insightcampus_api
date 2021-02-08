using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace insightcampus_api.Dao
{
    public class ClassRepository : ClassInterface
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
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

        public async Task Update(ClassModel classModel)
        {
            _context.Entry(classModel).Property(x => x.class_nm).IsModified = true;
            _context.Entry(classModel).Property(x => x.teacher).IsModified = true;
            _context.Entry(classModel).Property(x => x.category).IsModified = true;
            _context.Entry(classModel).Property(x => x.duration).IsModified = true;
            _context.Entry(classModel).Property(x => x.duration_nm).IsModified = true;
            _context.Entry(classModel).Property(x => x.thumbnail).IsModified = true;
            _context.Entry(classModel).Property(x => x.price).IsModified = true;
            _context.Entry(classModel).Property(x => x.real_price).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTemplate(ClassModel classModel)
        {
            _context.Entry(classModel).Property(x => x.template).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters)
        {
            var result = (
                      from cls in _context.ClassContext
                    select cls);

            // result = result.OrderByDescending(o => o.reg_dt);

            foreach (var filter in filters)
            {
                if (filter.k == "class_nm")
                {
                    result = result.Where(w => w.class_nm.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "teacher")
                {
                    result = result.Where(w => w.teacher == Convert.ToInt16(filter.v));
                }

                else if (filter.k == "duration_nm")
                {
                    result = result.Where(w => w.duration_nm.Contains(filter.v.Replace(" ", "")));
                }
            }

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }


        public async Task<ClassModel> Select(int class_seq)
        {
            var result = await (
                      from cls in _context.ClassContext
                     where cls.class_seq == class_seq
                    select cls).SingleAsync();

            return result;
        }

    }
}
