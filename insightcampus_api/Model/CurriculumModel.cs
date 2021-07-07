using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("curriculum")]
    public class CurriculumModel
    {
        [Key]
        public int curriculum_seq { get; set; }
        public string curriculum_nm { get; set; }
        public int curriculumgroup_seq { get; set; }
        public int order { get; set; }
        public string type { get; set; }
        public string option { get; set; }
        public string url { get; set; }
        public string duration { get; set; }
    }
}
