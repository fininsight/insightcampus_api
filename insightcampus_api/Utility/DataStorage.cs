using insightcampus_api.Model;
using System.Collections.Generic;
using System;

namespace insightcampus_api.Utility
{
    public static class DataStorage
    {
        public static SpecModel GetSpecData() =>
                new SpecModel
                {
                    spec_seq = 1,
                    tutor_name = "이진범",
                    spec_type = "멘토링비",
                    year = 2020,
                    month = 7,
                    lec_wage_per_hour = 8,
                    lec_time = 16,
                    mnt_wage_per_hour = 3,
                    mnt_time = 16,
                    tax_percent = 0.033,
                    account_bank = "KB국민",
                    account_num = "277237-04-001089"
                };

        public static string ToAccounting(double money)
        {
            return String.Format("{0:#,0}", money);
        }
    }
}