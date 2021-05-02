using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface ClassStudentInterface
    {
        Task<DataTableOutDto> Select(int class_seq, DataTableInputDto dataTableInputDto);
    }
}
