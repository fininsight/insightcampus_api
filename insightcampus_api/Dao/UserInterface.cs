using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface UserInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(UserModel userModel);
        UserModel Join(UserModel user);
        UserModel PasswordCheck(UserModel userModel, UserModel userMatched);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters);
        Task<UserModel> UserExists(UserModel userModel);
        Task<TeacherModel> FamilyExists(TeacherModel teacherModel);
    }
}
