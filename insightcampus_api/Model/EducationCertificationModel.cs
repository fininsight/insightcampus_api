using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("education_certification")]
    public class EducationCertificationModel
    {
        [Key]
        public int eduCertification_seq { get; set; }
        public DateTime eduCertification_date { get; set; }
        public int reg_user { get; set; }
        public DateTime reg_dt { get; set; }
        public int upd_user { get; set; }
        public DateTime upd_dt { get; set; }
        public int use_yn { get; set; }
        [NotMapped]
        public int number { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public string education_nm { get; set; }
        [NotMapped]
        public DateTime start_date { get; set; }
        [NotMapped]
        public DateTime end_date { get; set; }
        [NotMapped]
        public int term { get; set; }
    }
}
