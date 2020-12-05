using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface CommunityInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task Update(CommunityModel communityModel);
        Task UpdateTemplate(CommunityModel communityModel);
        Task IncreaseViewCount(CommunityModel communityModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<CommunityModel> Select(int board_seq);
    }
}
