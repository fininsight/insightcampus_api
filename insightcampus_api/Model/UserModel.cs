using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("user")]
    public class UserModel
    {
        [Key]
        public int user_seq { get; set; }
        public string user_id { get; set; }
        public string user_pw { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public int status { get; set; }
    }
}
