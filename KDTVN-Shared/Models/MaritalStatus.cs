using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class MaritalStatus
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

        public List<MaritalStatus> Get(MaritalStatus maritalStatus)
        {
            List<MaritalStatus> maritalStatuses = new List<MaritalStatus>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", maritalStatus.id ?? -1);
            dParam.Add("@name", maritalStatus.name ?? "");
            dParam.Add("@name_en", maritalStatus.name_en ?? "");
            dParam.Add("@name_jp", maritalStatus.name_jp ?? "");
            dParam.Add("@status", maritalStatus.status ?? null);
            dParam.Add("@description", maritalStatus.description ?? "");

            maritalStatuses = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<MaritalStatus>("[emp].[sp_get_marital_status]", dParam).ToList();

            return maritalStatuses;
        }

    }
}
