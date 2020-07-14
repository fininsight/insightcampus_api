using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface ClassQnaInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(ClassQnaModel classQnaModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<ClassQnaModel> Select(int class_qna_seq);
    }
}