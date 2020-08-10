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
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<TeacherModel> Select(int teacher_seq);
        Task<List<TeacherModel>> SelectTeacher(String searchText);
        Task Delete<T>(T entity) where T : class;
    }
}
