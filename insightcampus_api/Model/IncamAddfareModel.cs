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
        public int teacher_seq { get; set; }
        public int contract_seq { get; set; }
        public int hour { get; set; }
        public DateTime addfare_date { get; set; }
        public string income_type { get; set; }
        [NotMapped]
        public string original_company_nm { get; set; }
        [NotMapped]
        public string @class { get; set; }
        [NotMapped]
        public string name { get; set; }
    }
}
