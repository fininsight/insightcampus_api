using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("role")]
    public class RoleModel
    {
        [Key]
        public int role_seq { get; set; }
        public string role_nm { get; set; }
        public DateTime reg_dt { get; set; }
        public int reg_user { get; set; }
        public DateTime upd_dt { get; set; }
        public int upd_user { get; set; }
    }
}
