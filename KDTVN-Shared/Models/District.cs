using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class District
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

        public int? id_province { get; set; }

        public List<District> Get(District district)
        {
            List<District> districts = new List<District>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", district.id ?? -1);
            dParam.Add("@name", district.name ?? "");
            dParam.Add("@name_en", district.name_en ?? "");
            dParam.Add("@name_jp", district.name_jp ?? "");
            dParam.Add("@status", district.status ?? null);
            dParam.Add("@description", district.description ?? "");
            dParam.Add("@id_province", district.id_province ?? -1);

            districts = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<District>("[address].[sp_get_district]", dParam).ToList();

            return districts;
        }
    }

    public class Districts
    {
        public List<District> districts { get; set; }
        public Districts(List<District> districts)
        {
            this.districts = districts;
        }
    }
}
