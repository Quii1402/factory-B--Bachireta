using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using FactoryB.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Data.Common;


namespace FactoryB.Models
{
    public class RequestListModel
    {
        public int id_request { get; set; }
        public string item_code { get; set; }
        public string line_code { get; set; }
        public decimal request_qty { get; set; }
        public int read_qty { get; set; }
        public int id_status { get; set; }
        public string status_name { get; set; }
        public string description { get; set; }
        public string last_modify_by { get; set; }
        public DateTime? last_modify_at { get; set; }

        public QueryResult Insert(RequestListModel input)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@item_code", input.item_code);
            dParam.Add("@request_qty", input.request_qty);
            dParam.Add("@line_code", input.line_code);
            dParam.Add("@last_modify_by", input.last_modify_by);

            var result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureDataQueryResult("sp_ht_request_to_line", dParam);

            return result;
        }

        public QueryResult Delete(int id)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", id);
            var result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("sp_delete_request", dParam);

            return result;
        }

        public List<RequestListModel> Get()
        {

            var result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureData<RequestListModel>("sp_get_request").ToList();
            return result;
        }


    }
}







