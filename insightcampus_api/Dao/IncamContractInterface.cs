using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface IncamContractInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(IncamContractModel incamContractModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters);
        Task<List<IncamContractModel>> SelectExcel(List<Filter> filters);
        Task<IncamContractModel> Select(int contract_seq);
        Task<List<IncamContractModel>> SelectContract(String searchText);
        Task Delete(IncamContractModel incamContractModel);
    }
}
