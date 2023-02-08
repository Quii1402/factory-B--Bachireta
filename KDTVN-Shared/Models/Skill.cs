using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Skill
    {
        public int? id { get; set; }

        public string name { get; set; }

        public string name_en { get; set; }

        public string name_jp { get; set; }

        public string code { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public int? create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public int? last_modify_by { get; set; }

        public string description { get; set; }

        public string dual_name { get; set; }

        public List<Skill> Get(Skill skill)
        {
            List<Skill> skills = new List<Skill>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", skill.id ?? -1);
            dParam.Add("@name", skill.name ?? "");
            dParam.Add("@name_en", skill.name_en ?? "");
            dParam.Add("@name_jp", skill.name_jp ?? "");
            dParam.Add("@status", skill.status);
            dParam.Add("@description", skill.description ?? "");

            skills = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Skill>("[skill].[sp_get_category]", dParam).ToList();

            return skills;
        }
    }
}
