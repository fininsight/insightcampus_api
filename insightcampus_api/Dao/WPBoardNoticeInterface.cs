using System;
using System.Threading.Tasks;
using insightcampus_api.Data;

namespace insightcampus_api.Dao
{
    public interface WPBoardNoticeInterface
    {
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<DataTableOutDto> SelectLibrary(DataTableInputDto dataTableInputDto);
        Task<DataTableOutDto> SelectReview(DataTableInputDto dataTableInputDto);
        Task Update(string uid, string category);
        Task Init(string uid);
    }
}
