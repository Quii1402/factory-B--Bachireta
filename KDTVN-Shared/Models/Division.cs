using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Division
    {
        public int? id { get; set; }

        public int? id_department { get; set; }

        public string name { get; set; }

        public string name_en { get; set; }

        public string name_jp { get; set; }

        public string cost_center { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public int? create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public int? last_modify_by { get; set; }

        public string description { get; set; }

        public List<DivisionExtend> Get(Division division)
        {
            List<DivisionExtend> divisions = new List<DivisionExtend>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", division.id ?? -1);
            dParam.Add("@name", division.name ?? "");
            dParam.Add("@name_en", division.name_en ?? "");
            dParam.Add("@name_jp", division.name_jp ?? "");
            dParam.Add("@status", division.status ?? null);
            dParam.Add("@description", division.description ?? "");
            dParam.Add("@id_department", division.id_department ?? -1);
            dParam.Add("@cost_center", division.cost_center ?? "");

            divisions = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<DivisionExtend>("[kyo].[sp_get_division]", dParam).ToList();

            return divisions;
        }

    }

    public class DivisionExtend : Division
    {
        public string dual_name { get; set; }
    }
}
