using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("order_item")]
    public class OrderItemModel
    {
        [Key]
        public int order_item_seq { get; set; }
        public int order_id { get; set; }
        public int class_seq { get; set; }
        public decimal price { get; set; }
    }
}
