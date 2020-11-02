using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface EmailLogInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete(EmailLogModel emaillogModel);
        Task Update(EmailLogModel emaillogModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<EmailLogModel> Select(int email_log_seq);
    }
}
