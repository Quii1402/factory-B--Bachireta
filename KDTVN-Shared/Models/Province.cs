using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Province
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

        public List<Province> Get(Province province)
        {
            List<Province> provinces = new List<Province>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", province.id ?? -1);
            dParam.Add("@name", province.name ?? "");
            dParam.Add("@name_en", province.name_en ?? "");
            dParam.Add("@name_jp", province.name_jp ?? "");
            dParam.Add("@status", province.status ?? null);
            dParam.Add("@description", province.description ?? "");

            provinces = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Province>("[address].[sp_get_province]", dParam).ToList();

            return provinces;
        }
    }
}
