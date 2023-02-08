namespace KDTVN_Shared.Helper
{
    public class DBConnection
    {
        //KDTVN-BUSINESS : 10.170.162.45
        //TESTSVRVN2 : 10.170.162.38
        //KDTVN-PCMAN : 10.170.162.29

        #region KDTVN_MGMT
        //public const string KDTVN_MGMT = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_MGMT;User ID=sa;Password=123@4dmin";
        public const string KDTVN_MGMT = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_MGMT2;User ID=sa;Password=123@4dmin";
        //public const string KDTVN_MGMT = @"Data Source=.;Initial Catalog=KDTVN_MGMT;User ID=sa;Password=123@4dmin";
        #endregion

        public const string KDTVN_CANDIDATE = @"Data Source=10.170.162.29;Initial Catalog=KDTVN_CANDIDATE;User ID=sa;Password=123@4dmin";
        public const string KDTVN_IT_ASSET = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_IT_ASSET;User ID=sa;Password=123@4dmin";
        public const string KDTVN_CDC_TOOL = @"Data Source=10.170.162.45;Initial Catalog=CDC_TOOL;User ID=sa;Password=123@4dmin";
        public const string VN_LINE_INTERFACE = @"Data Source=10.170.162.35;Initial Catalog=LINE_JIG_DATA;User ID=lineuser;Password=lineuser";

        //MongoDB connection string
        public const string MONGODB_TEST_CONNSTR = "mongodb://sa:123%404dmin@10.170.162.154:27017/admin?authSource=admin&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false";
        public const string MONGODB_CONNSTR = "mongodb://sa:123%404dmin@10.170.162.154:27017/admin?authSource=admin&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false";

        public const string KDTVN_JIG_DATA_MONITORING = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_JIG_DATA_MONITORING;User ID=sa;Password=123@4dmin";
        public const string KDTVN_REALTIME_JIG = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_REALTIME_JIG;User ID=sa;Password=123@4dmin";

        public const string KDTVN_DATABASE = @"Data Source=10.170.162.29;Initial Catalog=KDTVN_DATABASE;User ID=sa;Password=123@4dmin";

        public const string KDTVN_PART_MANUFACTURING_MGMT = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_PART_MANUFACTURING_MGMT;User ID=sa;Password=123@4dmin";

        #region KDTVN_STOCK_MGMT_C
        /*Test DB*/
        //public const string KDTVN_STOCK_MGMT_C = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_STOCK_MGMT_C_TEST;User ID=sa;Password=123@4dmin";
        //public const string KDTVN_STOCK_MGMT_C = @"Data Source=.;Initial Catalog=KDTVN_STOCK_MGMT_C;User ID=sa;Password=123@4dmin";

        /*Production DB*/
        public const string KDTVN_STOCK_MGMT_C = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_STOCK_MGMT_C;User ID=sa;Password=123@4dmin";
        #endregion


        #region KDTVN_LOGISTIC_MGMT
        public const string KDTVN_LOGISTIC_MGMT = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_LOGISTIC_MGMT;User ID=sa;Password=123@4dmin";
        #endregion

        #region KDTVN_FACT_B_BACHIRETA
        public const string KDTVN_FACT_B_BACHIRETA = @"Data Source=10.170.162.45;Initial Catalog=KDTVN_FACT_B_BACHIRETA;User ID=sa;Password=123@4dmin";
        #endregion
    }
}
