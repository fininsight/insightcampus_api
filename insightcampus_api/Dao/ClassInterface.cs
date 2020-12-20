using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface ClassInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(ClassModel classModel);
        Task UpdateTemplate(ClassModel classModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters);
        Task<ClassModel> Select(int class_seq);
    }
}
