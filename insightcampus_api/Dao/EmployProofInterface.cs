using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface EmployProofInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Delete(EmployProofModel employproofModel);
        Task Update(EmployProofModel employproofModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<EmployProofModel> Select(int employee_proof_seq);
    }
}
