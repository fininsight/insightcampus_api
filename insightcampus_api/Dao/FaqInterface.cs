using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface FaqInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(FaqModel incamAddfareModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<FaqModel> Select(int faq_seq);
        Task<List<FaqModel>> SelectFaq(String searchText);
        Task Delete<T>(T entity) where T : class;
    }
}
