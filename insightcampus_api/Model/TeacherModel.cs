using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("teacher")]
    public class TeacherModel
    {
        [Key]
        public int teacher_seq { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int? user_seq { get; set; }
        public string passwd { get; set; }
        public int? reg_user { get; set; }
        public DateTime? ret_dt { get; set; }
        public int? upd_user { get; set; }
        public DateTime? upd_dt { get; set; }
        public int use_yn { get; set; }
    }
}
