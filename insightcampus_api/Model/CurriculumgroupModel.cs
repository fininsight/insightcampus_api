using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("curriculumgroup")]
    public class CurriculumgroupModel
    {
        [Key]
        public int curriculumgroup_seq { get; set; }
        public string curriculumgroup_nm { get; set; }
        public int class_seq { get; set; }
        
    }
}
