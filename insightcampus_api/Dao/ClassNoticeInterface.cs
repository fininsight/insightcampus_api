using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface ClassNoticeInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(ClassNoticeModel classNoticeModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<ClassNoticeModel> Select(int class_notice_seq);
    }
}
