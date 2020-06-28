using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("roleuser")]
    public class RoleUserModel
    {
        public int user_seq { get; set; }
        public string role_nm { get; set; }
        public DateTime reg_dt { get; set; }
        public int reg_user { get; set; }
    }
}
