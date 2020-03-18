using System;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public class RoleRepository:RoleInterface
    {

        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update(RoleModel roleModel)
        {
            throw new NotImplementedException();
        }
    }
}
