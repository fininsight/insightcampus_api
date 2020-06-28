using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace insightcampus_api.Dao
{
    public class RoleUserRepository: RoleUserInterface
    {

        private readonly DataContext _context;

        public RoleUserRepository(DataContext context)
        {            
            _context = context;
        }

        public async Task<List<RoleUserModel>> Select(int uid)
        {
            var result = await (from r in _context.RoleUserContext                              
                               where uid == r.user_seq
                              select r).ToListAsync();


            return result;
        }
    }
}
