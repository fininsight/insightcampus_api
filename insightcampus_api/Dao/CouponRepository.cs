using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace insightcampus_api.Dao
{
    public class CouponRepository : CouponInterface
    {
        private readonly DataContext _context;

        public CouponRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CouponModel couponModel)
        {
            _context.Entry(couponModel).Property(x => x.coupon_nm).IsModified = true;
            _context.Entry(couponModel).Property(x => x.type).IsModified = true;
            _context.Entry(couponModel).Property(x => x.start_date).IsModified = true;
            _context.Entry(couponModel).Property(x => x.end_date).IsModified = true;
            _context.Entry(couponModel).Property(x => x.upd_dt).IsModified = true;
            _context.Entry(couponModel).Property(x => x.upd_user).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto)
        {
            var result = (
                    from coupon in _context.CouponContext
                    where coupon.use_yn != 0
                    select coupon);
            

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<CouponModel> Select(int coupon_seq)
        {
            var result = await (
                      from coupon in _context.CouponContext
                      where coupon.coupon_seq == coupon_seq && coupon.use_yn != 0
                      select coupon).SingleAsync();

            return result;
        }

        public async Task<List<CouponModel>> SelectCoupon(String searchText)
        {
            var result = (
                    from coupon in _context.CouponContext
                    where coupon.use_yn != 0
                    select coupon);

            if (searchText != "ALL")
            {
                result = result.Where(t => t.coupon_nm.Contains(searchText));
            }

            return await result.ToListAsync();
        }

        public async Task Delete(CouponModel couponModel)
        {
            _context.Entry(couponModel).Property(x => x.upd_dt).IsModified = true;
            _context.Entry(couponModel).Property(x => x.upd_user).IsModified = true;
            _context.Entry(couponModel).Property(x => x.use_yn).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}