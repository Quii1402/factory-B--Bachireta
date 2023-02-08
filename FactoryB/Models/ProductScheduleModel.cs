using Dapper;
using KDTVN_Shared.Helper;

namespace FactoryB.Models
{
    public class ProductScheduleModel
    {
        public int id { get; set; }
        public string line_code { get; set; }
        public string product_item { get; set; }
        public int seq { get; set; }
        public string status { get; set; }
        public List<ProductScheduleModel> Get(ProductScheduleModel input)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@line_code", input.line_code);
            dParam.Add("@product_item", input.product_item);

            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureData<ProductScheduleModel>("dbo.sp_get_product_schedule",dParam).ToList();
        }
        public QueryResult UpdateStatusComplete(int id)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", id);
            
            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureDataQueryResult("dbo.sp_update_product_complete", dParam);
        }
        public QueryResult UpdateStatusProduct(int id)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", id);

            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureDataQueryResult("[dbo].[sp_update_product_product]", dParam);
        }
        public List<ProductScheduleModel> GetProductItem(string line_code)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@line_code", line_code);
            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureData<ProductScheduleModel>("dbo.sp_get_productitem", dParam).ToList();
        }

    }
}
