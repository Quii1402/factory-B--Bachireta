using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class School
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

        public List<School> Get(School school)
        {
            List<School> schools = new List<School>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", school.id ?? -1);
            dParam.Add("@name", school.name ?? "");
            dParam.Add("@name_en", school.name_en ?? "");
            dParam.Add("@name_jp", school.name_jp ?? "");
            dParam.Add("@status", school.status ?? null);
            dParam.Add("@description", school.description ?? "");

            schools = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<School>("[edu].[sp_get_school]", dParam).ToList();

            return schools;
        }
    }
}
