using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("fin_work")]
    public class FinWorkModel
    {
        [Key]
        public int work_seq { get; set; }
        public string work_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public double expected_sales { get; set; }
        public double expected_purchase { get; set; }
        public double sales { get; set; }
        public double purchase { get; set; }
    }
}
