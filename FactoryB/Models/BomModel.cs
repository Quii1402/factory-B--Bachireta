using Dapper;
using FactoryB.Controllers;
using KDTVN_Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using FactoryB.Models;


namespace FactoryB.Models
{
    public class BomModel
    {
        public string? product_item { get; set; }
        public string? parts_item { get; set; }
        public string? type_item { get; set; }
        public string? box_type { get; set; }
        public int? qty_per_box { get; set; }
        public bool? status { get; set; }
        public string? last_modify_by { get; set; }
        public DateTime? last_modify_at { get; set; }

        //Get Bom
        public List<BomModel> GetBomModel(BomModel bom)
        {
            List<BomModel> result = new List<BomModel>();
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@product_item", bom.product_item);
            dParam.Add("@parts_item", bom.parts_item);
            dParam.Add("@type_item", bom.type_item);
            dParam.Add("@box_type", bom.box_type);
            dParam.Add("@qty_per_box", bom.qty_per_box);
            dParam.Add("@status", bom.status);
            dParam.Add("@last_modify_by", bom.last_modify_by);
            dParam.Add("@last_modify_at", bom.last_modify_at);

            result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureData<BomModel>("[dbo].[sp_get_bom]", dParam).ToList();

            return result;
        }
        //Get Bom
        public List<BomModel> GetBomModelByProductItem(BomModel bom)
        {
            List<BomModel> result = new List<BomModel>();
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@product_item", bom.product_item);
            dParam.Add("@type_item", bom.type_item);
            result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureData<BomModel>("[dbo].[sp_get_itemcode_by_product_item]", dParam).ToList();

            return result;
        }
        //Delete Bom

        public QueryResult DeleteBomModel(BomModel bom)
        {
            QueryResult qr = new QueryResult();

            DynamicParameters dParam = new();

            dParam.Add("@product_item", bom.product_item);
        
            qr = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("[dbo].[sp_delete_bom]", dParam);

            return qr;
        }

        //UPDATE  
        public QueryResult UpdateBomModel(BomModel bom)
        {

            QueryResult qr = new QueryResult();

            DynamicParameters dParam = new DynamicParameters();

            dParam.Add("@product_item", bom.product_item);
            dParam.Add("@parts_item", bom.parts_item);
            dParam.Add("@type_item", bom.type_item);
            dParam.Add("@box_type", bom.box_type);
            dParam.Add("@qty_per_box", bom.qty_per_box);
            dParam.Add("@status", bom.status);
            dParam.Add("@last_modify_by", bom.last_modify_by);
            dParam.Add("@last_modify_at", bom.last_modify_at);
         
            qr = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("[dbo].[sp_update_bom]", dParam);

            return qr;
        }

        public List<BomModel> GetAll()
        {
            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureData<BomModel>("dbo.sp_get_all_bom").ToList();

        }


    }

}






