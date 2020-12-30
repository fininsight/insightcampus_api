using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface FinWorkDetailInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(FinWorkDetailModel incamAddfareModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<FinWorkDetailModel> Select(int work_seq);
        Task Delete(FinWorkDetailModel finWorkDetailModel);
    }
}
