using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface RoleUserInterface
    {
        Task<List<RoleUserModel>> Select(int uid);
    }
}
