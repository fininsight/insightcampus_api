using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("incam_employee_proof")]
    public class EmployProofModel
    {
        [Key]
        public int employee_proof_seq { get; set; }
        public String name { get; set; }
        public String register_number { get; set; }
        public String home_address { get; set; }
        public String phone_number { get; set; }
        public String department { get; set; }
        public String work_address { get; set; }
        public String position { get; set; }
        public String work_period { get; set; }
        public String purpose { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime upd_date { get; set; }
        public int download_count { get; set; }

    }
}
