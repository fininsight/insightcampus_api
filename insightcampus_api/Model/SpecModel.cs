using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    //[Table("spec")]
    public class SpecModel
    {
        //[Key]
        public int spec_seq { get; set; }
        public string tutor_name { get; set; }
        public string spec_type { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int lec_wage_per_hour { get; set; }
        public int lec_time { get; set; }
        public int mnt_wage_per_hour { get; set; }
        public int mnt_time { get; set; }
        public double tax_percent { get; set; }
        public string account_bank { get; set; }
        public string account_num { get; set; }
    }
}
