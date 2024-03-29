﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface IncamAddfareInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(IncamAddfareModel incamAddfareModel);
        Task UpdateDeposit(List<IncamAddfareModel> incamAddfareModels);
        Task UpdateEvidenceType(List<IncamAddfareModel> incamAddfareModels);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters);
        Task<List<IncamAddfareModel>> SelectExcel(List<Filter> filters);
        Task<IncamAddfareModel> Select(int addfare_seq);
        Task<DataTableOutDto> SelectFamily(DataTableInputDto dataTableInputDto, int teacher_seq);
        Task Delete(IncamAddfareModel incamAddfareModel);
        Task SendAddfare(List<IncamAddfareModel> incamAddfares);
    }
}
