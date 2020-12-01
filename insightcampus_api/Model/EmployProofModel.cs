using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("employ_proof")]
    public class EmployProofModel
    {
        [Key]
        public int name { get; set; }
        public String employ_date { get; set; }
    }
}
