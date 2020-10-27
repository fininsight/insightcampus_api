using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("email_log")]
    public class EmailLogModel
    {
        [Key]
        public int email_log_seq { get; set; }
        public string subject { get; set; }
        public string contents { get; set; }
        public string to { get; set; }
        public DateTime reg_date { get; set; }
        public int use_yn { get; set; }
    }
}

