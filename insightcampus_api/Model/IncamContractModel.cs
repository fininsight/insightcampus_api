using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insightcampus_api.Model
{
    [Table("incam_contract")]
    public class IncamContractModel
    {
        [Key]
        public int contract_seq { get; set; }
        public string @class { get; set; }
        public string original_company { get; set; }
        public decimal hour_price { get; set; }
        public decimal hour_incen { get; set; }
        public decimal contract_price { get; set; }
        public DateTime contract_start_date { get; set; }
        public DateTime contract_end_date { get; set; }
        [NotMapped]
        public string original_company_nm { get; set; }
    }
}
