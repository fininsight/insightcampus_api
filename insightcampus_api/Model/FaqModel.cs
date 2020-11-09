using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("faq")]
    public class FaqModel
    {
        [Key]
        public int faq_seq { get; set; }
        public string faq_nm { get; set; }
        public string content { get; set; }
        public string reg_user { get; set; }
        public DateTime reg_dt { get; set; }
        public string upd_user { get; set; }
        public DateTime upd_dt { get; set; }
    }
}
