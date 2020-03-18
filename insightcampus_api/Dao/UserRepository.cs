using System;
using insightcampus_api.Data;
using insightcampus_api.Model;
using System.Linq;
using System.Collections.Generic;

namespace insightcampus_api.Dao
{
    public class UserRepository: UserInterface
    {

        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public List<UserModel> Select()
        {
            var result = (
                            from user in _context.UserContext
                          select user).ToList();

            return result;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
