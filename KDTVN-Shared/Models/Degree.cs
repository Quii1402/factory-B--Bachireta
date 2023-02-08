using System;

namespace KDTVN_Shared.Models
{
    public class Degree
    {
        public int? id { get; set; }

        public int? id_user { get; set; }

        public int? id_degree_level { get; set; }

        public int? id_school { get; set; }

        public DateTime? graduate_at { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public int? create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public int? last_modify_by { get; set; }

        public string description { get; set; }

        public DateTime? study_from { get; set; }

        public decimal? gpa { get; set; }

        public string specialized { get; set; }
    }

    public class DegreeLevel
    {
        public int? id { get; set; }

        public string name { get; set; }

        public string name_en { get; set; }

        public string name_jp { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public int? create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public int? last_modify_by { get; set; }

        public string description { get; set; }

        public string dual_name { get; set; }
    }

    public class Education : Degree
    {
        public string school_name { get; set; }
    }
}
