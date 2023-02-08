using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Department
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

        public string code { get; set; }

        public List<DepartmentExtend> Get(Department department)
        {
            List<DepartmentExtend> departments = new List<DepartmentExtend>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", department.id ?? -1);
            dParam.Add("@name", department.name ?? "");
            dParam.Add("@name_en", department.name_en ?? "");
            dParam.Add("@name_jp", department.name_jp ?? "");
            dParam.Add("@status", department.status ?? null);
            dParam.Add("@description", department.description ?? "");
            dParam.Add("@code", department.code ?? "");

            departments = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<DepartmentExtend>("[kyo].[sp_get_department]", dParam).ToList();

            return departments;
        }
    }

    public class DepartmentExtend : Department
    {
        public string dual_name { get; set; }
    }
}
