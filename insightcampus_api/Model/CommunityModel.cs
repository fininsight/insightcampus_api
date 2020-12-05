using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("board")]
    public class CommunityModel
    {
        [Key]
        public int board_seq { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int view_count { get; set; }
        public int reg_user { get; set; }
        public DateTime reg_dt { get; set; }
        public int upd_user { get; set; }
        public DateTime upd_dt { get; set; }
    }
}
