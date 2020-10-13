using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class TeacherRepository : TeacherInterface
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TeacherModel teacherModel)
        {
            _context.Entry(teacherModel).Property(x => x.name).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.email).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.phone).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.address).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.user_seq).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.passwd).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.reg_user).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.ret_dt).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.upd_user).IsModified = true;
            _context.Entry(teacherModel).Property(x => x.upd_dt).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from teacher in _context.TeacherContext
                    where teacher.use_yn == 1
                    orderby teacher.name
                  select teacher);
            

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<TeacherModel> Select(int teacher_seq)
        {
            var result = await (
                      from teacher in _context.TeacherContext
                     where teacher.teacher_seq == teacher_seq
                     where teacher.use_yn == 1
                    select teacher).SingleAsync();

            return result;
        }

        public async Task<List<TeacherModel>> SelectTeacher(String searchText)
        {
            var result = (
                    from teacher in _context.TeacherContext
                    where teacher.use_yn == 1
                  select teacher);

            if (searchText != "ALL")
            {
                result = result.Where(t => t.name.Contains(searchText));
            }

            return await result.ToListAsync();
        }

        //public async Task Delete<T>(T entity) where T : class
        //{
        //    _context.Remove(entity);
        //    await _context.SaveChangesAsync();
        //}

        public async Task Delete(TeacherModel teacher)
        {
            _context.Entry(teacher).Property(x => x.use_yn).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLog(TeacherModel entity)
        {
            TeacherLogModel teacher_log = new TeacherLogModel();
            teacher_log.action = 1;
            teacher_log.log_dt = DateTime.Now;

            teacher_log.teacher_seq = entity.teacher_seq;
            teacher_log.name = entity.name;
            teacher_log.email = entity.email;
            teacher_log.phone = entity.phone;
            teacher_log.address = entity.address;
            teacher_log.user_seq = entity.user_seq;
            teacher_log.passwd = entity.passwd;

            await _context.AddAsync(teacher_log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLog(int teacher_seq)
        {
            TeacherLogModel teacher_log = new TeacherLogModel();
            teacher_log.action = 2;
            teacher_log.log_dt = DateTime.Now;

            var result = await (
                      from teacher in _context.TeacherContext
                      where teacher.teacher_seq == teacher_seq
                      select teacher).SingleAsync();

            teacher_log.teacher_seq = teacher_seq;
            teacher_log.name = result.name;
            teacher_log.email = result.email;
            teacher_log.phone = result.phone;
            teacher_log.address = result.address;
            teacher_log.user_seq = result.user_seq;
            teacher_log.passwd = result.passwd;

            await _context.AddAsync(teacher_log);
            await _context.SaveChangesAsync();
        }
    }
}