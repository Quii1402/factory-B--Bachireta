using Dapper;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using FactoryB.Models;


namespace FactoryB.Models
{
    public class InStockModel
    {
        public int? id_instock { get; set; }
        public string? item_code { get; set; }
        public string? location_code { get; set; }
        public int? qty { get; set; }
        public string? instock_by { get; set; }
        public DateTime? instock_at { get; set; }
        public string? po_no { get; set; }
        public string? po_item { get; set; }
        public string? lot_no { get; set; }
        public string? delivery_no { get; set; }
        public string? date_range { get; set; }

        //Get INSTOCK
        public List<InStockModel> GetInStockModel(InStockModel ínstock)
        {
            List<InStockModel> result = new List<InStockModel>();
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id_instock", ínstock.id_instock);
            dParam.Add("@item_code", ínstock.item_code);
            dParam.Add("@location_code", ínstock.location_code);
            dParam.Add("@qty", ínstock.qty);
            dParam.Add("@instock_by", ínstock.instock_by);
            dParam.Add("@instock_at", ínstock.instock_at);
            dParam.Add("@po_no", ínstock.po_no);
            dParam.Add("@po_item", ínstock.po_item);
            dParam.Add("@lot_no", ínstock.lot_no);
            dParam.Add("@delivery_no", ínstock.delivery_no);
            dParam.Add("@date_range", ínstock.date_range);


            result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureData<InStockModel>("[dbo].[sp_get_instock_fix]", dParam).ToList();

            return result;
        }
        //Delete Instock
        public QueryResult DeleteInstockData(InStockModel ínstock)
        {
            QueryResult qr = new QueryResult();

            DynamicParameters dParam = new();

            dParam.Add("@id_instock", ínstock.id_instock);

            qr = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("[dbo].[sp_delete_íntock]", dParam);

            return qr;
        }
    }
}






