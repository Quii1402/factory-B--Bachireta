using Dapper;
using KDTVN_Shared.Helper;

namespace FactoryB.Models
{
    public class ScreenRequestModel
    {
        public int id_request { get; set; }

        public string item_code { get; set; }

        public string line_code { get; set; }

        public string item_text { get; set; }

        public string type_item { get; set; }
        public int request_qty { get; set; }

        public int? read_qty { get; set; }

        public string status_name { get; set; }

        public int? qty { get; set; }

        public string location_code { get; set; }


        public string po_no { get; set; }

        public string po_item { get; set; }

        public string lot_no { get; set; }

        public string delivery_no { get; set; }

        public long? rank { get; set; }
        public List<ScreenRequestModel> Get(string line,string floor)
        {
            try
            {
                DynamicParameters dParam = new DynamicParameters();
                dParam.Add("@line_code", line);
                dParam.Add("@floor", floor);
                List<ScreenRequestModel> cartRequests = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureData<ScreenRequestModel>("[dbo].[sp_get_request_on_screen]", dParam).ToList();

                return cartRequests;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
