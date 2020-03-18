using System;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public class CategoryClass: CategoryInterface
    {
        public CategoryClass()
        {
        }

        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryModel categoryModel)
        {
            throw new NotImplementedException();
        }
    }
}
