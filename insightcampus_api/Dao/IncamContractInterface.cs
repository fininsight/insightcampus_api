using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface IncamContractInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(IncamContractModel incamContractModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<IncamContractModel> Select(int contract_seq);
        Task Delete<T>(T entity) where T : class;
    }
}
