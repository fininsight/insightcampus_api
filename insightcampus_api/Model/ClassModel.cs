using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("class")]
    public class ClassModel
    {
        [Key]
        public int class_seq { get; set; }
        public string class_nm { get; set; }
        public int? teacher { get; set; }
        public int? category { get; set; }
        public int duration { get; set; }
        public string duration_nm { get; set; }
        public string thumbnail { get; set; }
        public int price { get; set; }
        public int real_price { get; set; }
        public string template { get; set; }
    }
}
