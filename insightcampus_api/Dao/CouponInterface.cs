using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace insightcampus_api.Dao
{
    public interface CouponInterface
    {
        Task Add<T>(T entity) where T : class;
        Task Update(CouponModel incamAddfareModel);
        Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto);
        Task<CouponModel> Select(int coupon_seq);
        Task<List<CouponModel>> SelectCoupon(String searchText);
        Task Delete(CouponModel couponModel);
    }
}
