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
        Task SendCertification(int class_seq, int order_user_seq, string file_path);
    }
}
