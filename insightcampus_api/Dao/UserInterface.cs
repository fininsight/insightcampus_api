using System;
using System.Collections.Generic;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface UserInterface
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update(UserModel userModel);
        List<UserModel> Select();
    }
}
