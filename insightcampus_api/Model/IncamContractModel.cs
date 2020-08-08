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
        public int contract_day { get; set; }
        public int addfare_seq { get; set; }
    }
}
