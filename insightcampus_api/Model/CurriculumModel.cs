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
        public int class_seq { get; set; }
        public int duration { get; set; }
        public int level { get; set; }
        public int parent_seq { get; set; }
        public int order { get; set; }
        public string video_url { get; set; }
        public string video_type { get; set; }
    }
}
