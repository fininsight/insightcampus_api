using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("coupon")]
    public class CouponModel
    {
        [Key]
        public int coupon_seq { get; set; }
        public string coupon_nm { get; set; }
        public string type { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime reg_dt { get; set; }
        public string reg_user { get; set; }
        public DateTime upd_dt { get; set; }
        public string upd_user { get; set; }
        public int use_yn { get; set; }
    }
}
