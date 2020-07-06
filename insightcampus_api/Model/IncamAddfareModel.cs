using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("incam_addfare")]
    public class IncamAddfareModel
    {
        [Key]
        public int addfare_seq { get; set; }
        public DateTime addfare_date { get; set; }
        public string original_company { get; set; }
        public string @class { get; set; }
        public int gubun { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int hour { get; set; }
        public float tax { get; set; }
        public int income_type { get; set; }
        public int remit { get; set; }
    }
}
