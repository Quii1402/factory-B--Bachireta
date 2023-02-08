using KDTVN_Shared.Helper;

namespace FactoryB.Models
{
    public class LineModel
    {
        public string line_code { get; set; }
        public string line_name { get; set; }
        public decimal? status { get; set; }
        public string last_modify_by { get; set; }
        public DateTime? last_modify_date { get; set; }
        public string description { get; set; }
        public List<LineModel> GetAll()
        {
            return new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA)
                .ExecProcedureData<LineModel>("[dbo].[sp_get_line]").ToList();
        }

    }
}
