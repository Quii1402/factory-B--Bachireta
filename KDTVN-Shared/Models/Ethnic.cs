using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Ethnic
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

        public List<Ethnic> Get(Ethnic ethnic)
        {
            List<Ethnic> ethnics = new List<Ethnic>();
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", ethnic.id ?? -1);
            dParam.Add("@name", ethnic.name ?? "");
            dParam.Add("@name_en", ethnic.name_en ?? "");
            dParam.Add("@name_jp", ethnic.name_jp ?? "");
            dParam.Add("@status", ethnic.status ?? null);
            dParam.Add("@description", ethnic.description ?? "");

            ethnics = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Ethnic>("[emp].[sp_get_ethnic]", dParam).ToList();

            return ethnics;
        }
    }
}
