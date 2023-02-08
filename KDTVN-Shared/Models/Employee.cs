using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTVN_Shared.Models
{
    public class Employee
    {
        public string code { get; set; }

        public string fullname { get; set; }

        public string fullname_en { get; set; }

        public int? id_division { get; set; }

        public string code_nationality { get; set; }

        public int? id_gender { get; set; }

        public DateTime? birthdate { get; set; }

        public Guid? guid { get; set; }

        public int? id_position { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public string create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public string last_modify_by { get; set; }

        public string description { get; set; }

        public int? id_marital_status { get; set; }

        public int? id_id_card_info { get; set; }

        public string phone_number { get; set; }

        public DateTime? join_company_at { get; set; }

        public int? id_ethnic { get; set; }

        public string email { get; set; }

        public DateTime? graduate_date { get; set; }

        public string rank { get; set; }

        public List<Employee> GetEmployee(EmployeeSearch employee)
        {
            List<Employee> employees = new List<Employee>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@code", employee.code);
            dParam.Add("@fullname", employee.fullname);
            dParam.Add("@fullname_en", employee.fullname_en);
            dParam.Add("@id_division", employee.id_division);
            dParam.Add("@code_nationality", employee.code_nationality);
            dParam.Add("@id_gender", employee.id_gender);
            dParam.Add("@from_birthdate", employee.from_birthdate);
            dParam.Add("@to_birthdate", employee.to_birthdate);
            dParam.Add("@guid", employee.guid);
            dParam.Add("@id_position", employee.id_position);
            dParam.Add("@status", employee.status);
            dParam.Add("@description", employee.description);
            dParam.Add("@id_marital_status", employee.id_marital_status);
            dParam.Add("@id_id_card_info", employee.id_id_card_info);
            dParam.Add("@phone_number", employee.phone_number);
            dParam.Add("@from_join_company_at", employee.from_join_company_at);
            dParam.Add("@to_join_company_at", employee.to_join_company_at);
            dParam.Add("@id_ethnic", employee.id_ethnic);
            dParam.Add("@email", employee.email);
            dParam.Add("@from_graduate_date", employee.from_graduate_date);
            dParam.Add("@to_graduate_date", employee.to_graduate_date);
            dParam.Add("@rank", employee.rank);

            employees = (List<Employee>)new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Employee>("emp.sp_get_employee", dParam);

            return employees;
        }

        public Employee GetEmployeesByUsername (string username)
        {
            try
            {
                DynamicParameters dParam = new DynamicParameters();
                dParam.Add("@username", username);

                var employees = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Employee>("emp.sp_get_employee_by_username", dParam).ToList();
                return employees.Count == 1 ? employees.First() : null;
            }catch (Exception ex)
            {
                return null;
            }
        }
        public Employee GetEmployeeByAccount(string user, string pass)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@username", user);
            dParam.Add("@password", pass);

            var employees = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Employee>("si.sp_validate_login", dParam).ToList();
            return employees.Count == 1 ? employees.First() : null;
        }
    }

    public class EmployeeSearch : Employee
    {
        public string from_birthdate { get; set; }
        public string to_birthdate { get; set; }
        public string from_join_company_at { get; set; }
        public string to_join_company_at { get; set; }
        public string from_graduate_date { get; set; }
        public string to_graduate_date { get; set; }
    }
}
