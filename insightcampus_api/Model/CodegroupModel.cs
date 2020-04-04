using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("codegroup")]
    public class CodegroupModel
    {
        [Key]
        public string codegroup_id { get; set; }
        public string codegroup_nm { get; set; }
        public string description { get; set; }
        public DateTime reg_dt { get; set; }
        public int reg_user { get; set; }
        public DateTime upd_dt { get; set; }
        public int upd_user { get; set; }
    }
}
