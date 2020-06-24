using System.Threading.Tasks;
using insightcampus_api.Data;

namespace insightcampus_api.Dao
{
    public interface ClassReviewInterface
    {
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
    }
}
