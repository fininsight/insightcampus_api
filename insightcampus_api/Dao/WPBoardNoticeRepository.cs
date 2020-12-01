using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using insightcampus_api.Data;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace insightcampus_api.Dao
{
    public class WPBoardNoticeRepository: WPBoardNoticeInterface
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public WPBoardNoticeRepository(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            string sql = @"
                SELECT uid, title, category1, category2, date
                  FROM insightcampus.wp_kboard_board_content
                 WHERE (category1 != '자료실' OR category1 != '수강생후기')
              ORDER BY uid DESC
            ";

            var result = _context.WPBoardNoticeModel.FromSql(sql);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<DataTableOutDto> SelectLibrary(DataTableInputDto dataTableInputDto)
        {
            string sql = @"
                SELECT uid, title, category1, category2, date
                  FROM insightcampus.wp_kboard_board_content
                 WHERE category1 = '자료실'
              ORDER BY uid DESC
            ";

            var result = _context.WPBoardNoticeModel.FromSql(sql);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<DataTableOutDto> SelectReview(DataTableInputDto dataTableInputDto)
        {
            string sql = @"
                SELECT uid, title, category1, category2, date
                  FROM insightcampus.wp_kboard_board_content
                 WHERE category1 = '수강생후기'
              ORDER BY uid DESC
            ";

            var result = _context.WPBoardNoticeModel.FromSql(sql);

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task Update(string uid, string category)
        {
            string sql = $@"
                UPDATE insightcampus.wp_kboard_board_content
                   SET category1 = '{category}' 
                 WHERE uid = {uid}
            ";

            _context.Database.ExecuteSqlCommand(sql);
         
        }

        public async Task Init(string uid)
        {
            string sql = $@"
                UPDATE insightcampus.wp_kboard_board_content
                   SET category1 = '' 
                 WHERE uid = {uid}
            ";

            _context.Database.ExecuteSqlCommand(sql);

        }
    }
}
