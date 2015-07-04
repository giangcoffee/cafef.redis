using System;
using System.Data;
namespace CafeF.TA.DAL
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
        public DataTable TA_GetTopTenSignal(string symbol, DateTime dt, int idx, int pcount)
        {
            IDbCommand cmd = _db.CreateCommand("TA_GetTopTenSignal", true);
            _db.AddParameter(cmd, "Symbol", DbType.String, symbol);
            _db.AddParameter(cmd, "DateView", DbType.DateTime, dt);
            _db.AddParameter(cmd, "PageIndex", DbType.Int32, idx);
            _db.AddParameter(cmd, "PageCount", DbType.Int32, pcount);
            DataTable ret = CreateDataTable(cmd);
            return ret;
        }

        #endregion
    }
}