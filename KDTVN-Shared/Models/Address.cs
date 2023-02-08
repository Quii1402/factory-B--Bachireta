using System;

namespace KDTVN_Shared.Models
{
    public class Address
    {
        public int id { get; set; }

        public int? id_perm_province { get; set; }

        public int? id_perm_district { get; set; }

        public int? id_perm_ward { get; set; }

        public int? id_temp_province { get; set; }

        public int? id_temp_district { get; set; }

        public int? id_temp_ward { get; set; }

        public int? id_candidate { get; set; }

        public string perm_other_info { get; set; }

        public string temp_other_info { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public int? create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public int? last_modify_by { get; set; }

        public string description { get; set; }
    }
}
