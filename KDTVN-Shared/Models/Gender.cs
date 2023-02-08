using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Gender
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

        public List<Gender> Get(Gender gender)
        {
            List<Gender> genders = new List<Gender>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", gender.id ?? -1);
            dParam.Add("@name", gender.name ?? "");
            dParam.Add("@name_en", gender.name_en ?? "");
            dParam.Add("@name_jp", gender.name_jp ?? "");
            dParam.Add("@status", gender.status ?? null);
            dParam.Add("@description", gender.description ?? "");

            genders = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Gender>("[emp].[sp_get_gender]", dParam).ToList();

            return genders;
        }
    }
}
