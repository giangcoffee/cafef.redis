using System;
using System.Data;
namespace CafeF.BCTC.DAL
{
    public class StoredProcedures
    {
        private MainDB _db;

        public StoredProcedures(MainDB db)
        {
            _db = db;
        }

        public MainDB Database
        {
            get { return _db; }
        }

        protected DataTable CreateDataTable(IDbCommand command)
        {
            return _db.CreateDataTable(command);
        }

        protected DataSet CreateDataSet(IDbCommand command)
        {
            return _db.CreateDataSet(command);   
        }

        #region Procedure
        public DataTable sp_CafeF_DataCrawler_GetFinanceValue(string symbol, string type, int year, int quarter)
        {
            IDbCommand cmd = _db.CreateCommand("sp_CafeF_DataCrawler_GetFinanceValue", true);
            _db.AddParameter(cmd, "symbol", DbType.String, symbol);
            _db.AddParameter(cmd, "type", DbType.String, type);
            _db.AddParameter(cmd, "year", DbType.Int32, year);
            _db.AddParameter(cmd, "quarter", DbType.Int32, quarter);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(string symbol, string type, int year, int quarter)
        {
            IDbCommand cmd = _db.CreateCommand("sp_CafeF_DataCrawler_GetFinanceValue_Recent4part_v2", true);
            _db.AddParameter(cmd, "symbol", DbType.String, symbol);
            _db.AddParameter(cmd, "type", DbType.String, type);
            _db.AddParameter(cmd, "year", DbType.Int32, year);
            _db.AddParameter(cmd, "quarter", DbType.Int32, quarter);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(string symbol, string type, int year)
        {
            IDbCommand cmd = _db.CreateCommand("sp_CafeF_DataCrawler_GetFinanceValue_Recent4part_v2", true);
            _db.AddParameter(cmd, "symbol", DbType.String, symbol);
            _db.AddParameter(cmd, "type", DbType.String, type);
            _db.AddParameter(cmd, "year", DbType.Int32, year);
            _db.AddParameter(cmd, "quarter", DbType.Int32, 0);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }
        #endregion
    }
}