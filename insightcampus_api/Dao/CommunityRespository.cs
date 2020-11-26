using System;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class CommunityRepository : CommunityInterface
    {
        private readonly DataContext _context;

        public CommunityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CommunityModel communityModel)
        {
            _context.Entry(communityModel).Property(x => x.category).IsModified = true;
            _context.Entry(communityModel).Property(x => x.title).IsModified = true;
            _context.Entry(communityModel).Property(x => x.content).IsModified = true;
            _context.Entry(communityModel).Property(x => x.upd_dt).IsModified = true;
            _context.Entry(communityModel).Property(x => x.upd_user).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTemplate(CommunityModel communityModel)
        {
            _context.Entry(communityModel).Property(x => x.content).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from board in _context.CommunityContext
                    select board);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<CommunityModel> Select(int board_seq)
        {
            var result = await (
                      from board in _context.CommunityContext
                      where board.board_seq == board_seq
                      select board).SingleAsync();

            return result;
        }
    }
}
