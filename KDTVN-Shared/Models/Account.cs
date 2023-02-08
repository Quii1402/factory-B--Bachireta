using System;
using Dapper;
using KDTVN_Shared.Helper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace KDTVN_Shared.Models
{
    public class Account
    {
        public Account()
        {
        }

        public string emp_code { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public bool? login_with_master_pw { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public string create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public string last_modify_by { get; set; }

        public string description { get; set; }
    }

    public class AccountExtend : Account
    {
        public Employee EmpInfo { get; set; }
        public List<int> Permission { get; set; }
        public List<AccountSoftware> AccountSoftwares { get; set; }
        public Guid guid { get; set; }

        public AccountExtend() { }
        public AccountExtend(string user, string pass, int? idSoftware = null)
        {
            //Lấy thông tin emp dựa trên tài khoản
            Employee emp = new Employee().GetEmployeeByAccount(user, pass);

            if (emp != null)
            {
                this.username = user;
                EmpInfo = emp;
                emp_code = emp.code;
            }

            //Lấy thông tin quyền của tài khoản
            Permission = GetAccountObject(user);
            //Lấy thông tin ứng dụng của tài khoản (được truy cập vào hay không?)
            AccountSoftwares = new AccountSoftware().GetListAccountSoftwares(new AccountSoftware(user, idSoftware));
        }

        public AccountExtend(string user, int? idSoftware = null)
        {
            //Lấy thông tin emp dựa trên tài khoản
            Employee emp = new Employee().GetEmployeesByUsername(user);

            if (emp != null)
            {
                this.username = user;
                EmpInfo = emp;
                emp_code = emp.code;
            }

            //Lấy thông tin quyền của tài khoản
            Permission = GetAccountObject(user);
            //Lấy thông tin ứng dụng của tài khoản (được truy cập vào hay không?)
            AccountSoftwares = new AccountSoftware().GetListAccountSoftwares(new AccountSoftware(user, idSoftware));
        }

        public AccountExtend(Guid guid, int? idSoftware = null)
        {
            //Lấy thông tin user từ guid
            string user = GetUsernameFromGuid(guid);

            //Lấy thông tin emp dựa trên tài khoản
            Employee emp = new Employee().GetEmployeesByUsername(user);

            if (emp != null)
            {
                this.username = user;
                EmpInfo = emp;
                emp_code = emp.code;
            }

            //Lấy thông tin quyền của tài khoản
            Permission = GetAccountObject(user);
            //Lấy thông tin ứng dụng của tài khoản (được truy cập vào hay không?)
            AccountSoftwares = new AccountSoftware().GetListAccountSoftwares(new AccountSoftware(user, idSoftware));
        }

        public static Guid InsertLoginInfo(string username, string hostname)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@username", username);
            dParam.Add("@hostname", hostname);

            var guids = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<Guid>("si.sp_ins_login_info", dParam).ToList();

            return guids.FirstOrDefault();
        }

        private string GetUsernameFromGuid(Guid guid)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@guid", guid.ToString());

            var usernames = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<string>("si.sp_get_username_by_guid", dParam).ToList();

            return usernames.FirstOrDefault();
        }

        private List<int> GetAccountObject(string username)
        {
            List<int> per = new List<int>();

            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@username", username);
            dParam.Add("@id_object");

            per = new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<int>("si.sp_get_account_object", dParam).ToList();

            return per;
        }

    }

    public class AccountInfo : Account{
        public string fullname { get; set; }
        public string nationality { get; set; }
        public string division { get; set; }
        public string department { get; set; }
        public string position { get; set; }
        public string email { get; set; }

        public List<AccountInfo> GetAllAccount()
        {
            return new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<AccountInfo>("[si].[sp_get_account_all]").ToList();
        }
    }

    public class AccountSoftware
    {
        public int id { get; set; }

        public int? id_role { get; set; }

        public int? id_software { get; set; }

        public bool? status { get; set; }

        public DateTime? create_at { get; set; }

        public string create_by { get; set; }

        public DateTime? last_modify_at { get; set; }

        public string last_modify_by { get; set; }

        public string description { get; set; }

        public string username { get; set; }

        public string software_name { get; set; }

        public string link { get; set; }

        public AccountSoftware()
        {
        }

        public AccountSoftware(string user,int? idSoftware)
        {
            username = user;
            id_software = idSoftware;
        }
        public List<AccountSoftware> GetListAccountSoftwares(AccountSoftware input)
        {
            DynamicParameters dParam = new DynamicParameters();

            dParam.Add("@username", input.username);

            return new SQLHelper(DBConnection.KDTVN_MGMT).ExecProcedureData<AccountSoftware>("si.sp_get_account_software", dParam).ToList();
        }
    }
}
