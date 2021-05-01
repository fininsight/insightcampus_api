using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface CurriculumgroupInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(CurriculumgroupModel curriculumgroupModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
    }
}
