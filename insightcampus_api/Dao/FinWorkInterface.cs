using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface FinWorkInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(FinWorkModel incamAddfareModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<FinWorkModel> Select(int work_seq);
        Task<List<FinWorkModel>> SelectFinWork(String searchText);
        Task Delete(FinWorkModel finWorkModel);
    }
}
