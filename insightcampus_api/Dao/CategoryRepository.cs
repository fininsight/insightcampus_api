using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public class CategoryRepository: CategoryInterface
    {

        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
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

        public List<CodeModel> Select()
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryModel categoryModel)
        {
            throw new NotImplementedException();
        }

        public void Update(CodeModel codeModel)
        {
            throw new NotImplementedException();
        }
    }
}
