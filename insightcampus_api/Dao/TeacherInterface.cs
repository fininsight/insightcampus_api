using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface TeacherInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(TeacherModel incamAddfareModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters);
        Task<TeacherModel> Select(int teacher_seq);
        Task<List<TeacherModel>> SelectExcel(List<Filter> filters);
        Task<List<TeacherModel>> SelectTeacher(String searchText);
        Task Delete(TeacherModel teacher);
        Task UpdateLog(int teacher_seq);
    }
}
