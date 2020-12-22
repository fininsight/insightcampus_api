using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("fin_work_detail")]
    public class FinWorkDetailModel
    {
        [Key]
        public int work_detail_seq { get; set; }
        public int work_seq { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public double expected_sales { get; set; }
        public double expected_purchase { get; set; }
        public double sales { get; set; }
        public double purchase { get; set; }
    }
}
