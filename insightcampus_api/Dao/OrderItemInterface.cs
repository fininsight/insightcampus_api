using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insightcampus_api.Data;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface OrderItemInterface
    {
        Task AddList(List<OrderItemModel> orderItemList);
        Task AddList(List<OrderItemModel> orderItemList, int order_id);
        Task RemoveAll(int order_id);
        Task<List<OrderItemModel>> SelectItems(int order_id);
    }
}
