using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("category")]
    public class CategoryModel
    {
        [Key]
        public int category_seq { get; set; }
        public int category_parent { get; set; }
        public int category_dep { get; set; }
        public string category_nm { get; set; }
    }
}
