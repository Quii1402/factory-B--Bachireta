using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Ward
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

        public int? id_district { get; set; }

        public List<Ward> Get(Ward ward)
        {
            List<Ward> wards = new List<Ward>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", ward.id ?? -1);
            dParam.Add("@name", ward.name ?? "");
            dParam.Add("@name_en", ward.name_en ?? "");
            dParam.Add("@name_jp", ward.name_jp ?? "");
            dParam.Add("@status", ward.status ?? null);
            dParam.Add("@description", ward.description ?? "");
            dParam.Add("@id_district", ward.id_district ?? -1);

            wards = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Ward>("[address].[sp_get_wards]", dParam).ToList();

            return wards;
        }
    }
}
