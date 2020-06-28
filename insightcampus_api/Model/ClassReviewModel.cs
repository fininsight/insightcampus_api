﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("class_review")]
    public class ClassReviewModel
    {
        [Key]
        public int class_review_seq { get; set; }
        public int class_seq { get; set; }
        public int parent_seq { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int reg_user { get; set; }
        public DateTime reg_dt { get; set; }
        public int upd_user { get; set; }
        public DateTime upd_dt { get; set; }
    }
}