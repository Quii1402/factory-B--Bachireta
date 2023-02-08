using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class IDCardIssueBy
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

        public List<IDCardIssueBy> Get(IDCardIssueBy iDCardIssueBy)
        {
            List<IDCardIssueBy> listItem = new List<IDCardIssueBy>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", iDCardIssueBy.id ?? -1);
            dParam.Add("@name", iDCardIssueBy.name ?? "");
            dParam.Add("@name_en", iDCardIssueBy.name_en ?? "");
            dParam.Add("@name_jp", iDCardIssueBy.name_jp ?? "");
            dParam.Add("@status", iDCardIssueBy.status ?? null);
            dParam.Add("@description", iDCardIssueBy.description ?? "");

            listItem = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<IDCardIssueBy>("[emp].[sp_get_id_card_issue_place]", dParam).ToList();

            return listItem;
        }
    }
}
