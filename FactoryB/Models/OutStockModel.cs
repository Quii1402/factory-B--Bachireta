using Dapper;
using KDTVN_Shared.Helper;

namespace FactoryB.Models
{
    public class OutStockModel
    {
        public int id_outstock { get; set; }
        public int id_request { get; set; }
        public string line_code { get; set; }
        public string item_code { get; set; }
        public string item_text { get; set; }
        public string po_no { get; set; }
        public string po_item { get; set; }
        public string lot_no { get; set; }
        public string delivery_no { get; set; }
        public string location_code { get; set; }
        public int qty { get; set; }
        public int id_status { get; set; }
        public string status_name { get; set; }
        public string instock_by { get; set; }
        public DateTime? instock_at { get; set; }
        public string outstock_by { get; set; }
        public DateTime? outstock_at { get; set; }
        public string date_range { get; set; }
        public int floor { get; set; }

        public List<OutStockModel> Get(OutStockModel input)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@line_mulitiple", input.line_code);
            dParam.Add("@item_code", input.item_code);
            dParam.Add("@location_code", input.location_code);
            dParam.Add("@qty", input.qty);
            dParam.Add("@date_range", input.date_range);

            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureData<OutStockModel>("dbo.sp_get_outstock", dParam).ToList();
            
        }

        public QueryResult Insert(OutStockModel input)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@item_code", input.item_code);
            dParam.Add("@location_code", input.location_code);
            dParam.Add("@line_code", input.line_code);
            dParam.Add("@outstock_by", input.outstock_by);
            dParam.Add("@floor", input.floor);

            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("[dbo].[sp_ht_outstock_to_line]", dParam);
        }
    }
}
