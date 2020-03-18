using System;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface RoleInterface
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update(RoleModel roleModel);
    }
}
