using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("order")]
    public class OrderModel
    {
        [Key]
        public int order_id { get; set; }
        public DateTime order_date { get; set; }
        public decimal order_price { get; set; }
        public string order_type { get; set; }
        public string address { get; set; }
        public int reg_user { get; set; }
        public DateTime reg_date { get; set; }
        public int upd_user { get; set; }
        public DateTime upd_date { get; set; }
    }
}
