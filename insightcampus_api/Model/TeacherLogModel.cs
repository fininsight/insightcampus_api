using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("teacher_log")]
    public class TeacherLogModel
    {
        [Key]
        public int log_seq { get; set; }
        public int action { get; set; }
        public DateTime? log_dt { get; set; }
        public int teacher_seq { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int? user_seq { get; set; }
        public string passwd { get; set; }
    }
}
