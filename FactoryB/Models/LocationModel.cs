using Dapper;
using KDTVN_Shared.Helper;

namespace FactoryB.Models
{
    public class LocationModel
    {
        public string? location_code { get; set; }
        public int? status { get; set; }
        public string? description { get; set; }
        public string? last_modify_by { get; set; }
        public DateTime? last_modify_at { get; set; }
        public List<LocationModel> GetAll()
        {
            var result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureData<LocationModel>("[dbo].[sp_get_all_location]").ToList();
            return result;
        }

        public List<LocationModel> GetLocationCode(LocationModel input)
        {
            List<LocationModel> result = new List<LocationModel>();
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@location_code", input.location_code);            
            dParam.Add("@status", input.status);
            dParam.Add("@description", input.description);
            dParam.Add("@last_modify_by", input.last_modify_by);
            dParam.Add("@last_modify_at", input.last_modify_at);
            result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).
                ExecProcedureData<LocationModel>("[dbo].[sp_get_location]", dParam).ToList();
            return result;
        }

        public QueryResult InsertLocation(LocationModel input)
        {
            QueryResult qr = new QueryResult();
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@location_code", input.location_code);
            dParam.Add("@description", input.description);
            dParam.Add("@last_modify_by", input.last_modify_by);
            dParam.Add("@last_modify_at", input.last_modify_at);

            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureDataQueryResult("dbo.sp_insert_location",dParam);            
        }

        public QueryResult UpdateLocation(LocationModel input)
        {
            DynamicParameters dParam = new DynamicParameters();

            dParam.Add("@location_code", input.location_code);
            dParam.Add("@description", input.description);
            dParam.Add("@last_modify_by", input.last_modify_by);

            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureDataQueryResult("dbo.sp_update_location", dParam);
           
        }

        public QueryResult DeleteLocation(LocationModel input)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@location_code", input.location_code);
            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureDataQueryResult("dbo.sp_delete_location", dParam);
        }
    }
}
