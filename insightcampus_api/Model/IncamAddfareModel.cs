using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("incam_addfare")]
    public class IncamAddfareModel
    {
        [Key]
        public int addfare_seq { get; set; }
        public int contract_seq { get; set; }
        public float hour { get; set; }
        public DateTime addfare_date { get; set; }
        public string income_type { get; set; }
        public float income { get; set; }
        public int reg_user { get; set; }
        public DateTime reg_dt { get; set; }
        public int upd_user { get; set; }
        public DateTime upd_dt { get; set; }
        public int use_yn { get; set; }
        public int check_yn { get; set; }
        public string addfare_gubun { get; set; }
        public string go_check { get; set; }

        [NotMapped]
        public int teacher_seq { get; set; }
        [NotMapped]
        public string original_company_nm { get; set; }
        [NotMapped]
        public string @class { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public string income_type_nm { get; set; }
        [NotMapped]
        public decimal hour_price { get; set; }
        [NotMapped]
        public decimal hour_incen { get; set; }
        [NotMapped]
        public decimal contract_price { get; set; }
        [NotMapped]
        public float rate { get; set; }
        public string evidence_type { get; set; }
        [NotMapped]
        public string evidence_type_nm { get; set; }
        [NotMapped]
        public string addfare_gubun_nm { get; set; }
        [NotMapped]
        public string go_check_nm { get; set; }
    }
}
