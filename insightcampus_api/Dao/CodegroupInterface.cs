﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface CodegroupInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(CodegroupModel codegroupModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
    }
}
