using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("code")]
    public class CodeModel
    {
        public string code_id { get; set; }
        public string codegroup_id { get; set; }
        public string code_nm { get; set; }
        public string value1 { get; set; }
        public string value2 { get; set; }
        public string value3 { get; set; }
        public string value4 { get; set; }
        public string value5 { get; set; }
        public DateTime reg_dt { get; set; }
        public int reg_user { get; set; }
        public DateTime upd_dt { get; set; }
        public int upd_user { get; set; }
        public int order_num { get; set; }
    }
}
