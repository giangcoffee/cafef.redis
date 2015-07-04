using System;
using System.Data;
namespace CafeF.DLVM.DAL
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
        public DataTable pr_Chart_Year(Int64 parentid, int time1, int time2)
        {
            IDbCommand cmd = _db.CreateCommand("pr_Chart_Year", true);
            _db.AddParameter(cmd, "ParentID", DbType.Int64, parentid);
            _db.AddParameter(cmd, "Time1", DbType.Int32, time1);
            _db.AddParameter(cmd, "Time2", DbType.Int32, time2);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_Chart_Quarter(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            IDbCommand cmd = _db.CreateCommand("pr_Chart_Quarter", true);
            _db.AddParameter(cmd, "ParentID", DbType.Int64, parentid);
            _db.AddParameter(cmd, "Time1", DbType.Int32, time1);
            _db.AddParameter(cmd, "Time2", DbType.Int32, time2);
            _db.AddParameter(cmd, "Time3", DbType.Int32, time3);
            _db.AddParameter(cmd, "Time4", DbType.Int32, time4);
            _db.AddParameter(cmd, "Year1", DbType.Int32, year1);
            _db.AddParameter(cmd, "Year2", DbType.Int32, year2);
            _db.AddParameter(cmd, "Year3", DbType.Int32, year3);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_Chart_Month_CPI(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            IDbCommand cmd = _db.CreateCommand("pr_Chart_Month_CPI", true);
            _db.AddParameter(cmd, "ParentID", DbType.Int64, parentid);
            _db.AddParameter(cmd, "Time1", DbType.Int32, time1);
            _db.AddParameter(cmd, "Time2", DbType.Int32, time2);
            _db.AddParameter(cmd, "Time3", DbType.Int32, time3);
            _db.AddParameter(cmd, "Time4", DbType.Int32, time4);
            _db.AddParameter(cmd, "Year1", DbType.Int32, year1);
            _db.AddParameter(cmd, "Year2", DbType.Int32, year2);
            _db.AddParameter(cmd, "Year3", DbType.Int32, year3);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_IndexQuarter_SelectByIDAndTime(Int64 id, int time, int year)
        {
            IDbCommand cmd = _db.CreateCommand("pr_IndexQuarter_SelectByIDAndTime", true);
            _db.AddParameter(cmd, "ID", DbType.Int64, id);
            _db.AddParameter(cmd, "Time", DbType.Int32, time);
            _db.AddParameter(cmd, "Year", DbType.Int32, year);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_IndexYear_SelectByIDAndTime(Int64 id, int time)
        {
            IDbCommand cmd = _db.CreateCommand("pr_IndexYear_SelectByIDAndTime", true);
            _db.AddParameter(cmd, "ID", DbType.Int64, id);
            _db.AddParameter(cmd, "Time", DbType.Int32, time);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_Chart_Month(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            IDbCommand cmd = _db.CreateCommand("pr_Chart_Month", true);
            _db.AddParameter(cmd, "ParentID", DbType.Int64, parentid);
            _db.AddParameter(cmd, "Time1", DbType.Int32, time1);
            _db.AddParameter(cmd, "Time2", DbType.Int32, time2);
            _db.AddParameter(cmd, "Time3", DbType.Int32, time3);
            _db.AddParameter(cmd, "Time4", DbType.Int32, time4);
            _db.AddParameter(cmd, "Year1", DbType.Int32, year1);
            _db.AddParameter(cmd, "Year2", DbType.Int32, year2);
            _db.AddParameter(cmd, "Year3", DbType.Int32, year3);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_IndexMonth_SelectByIDAndTime(Int64 id, int time, int year)
        {
            IDbCommand cmd = _db.CreateCommand("pr_IndexMonth_SelectByIDAndTime", true);
            _db.AddParameter(cmd, "ID", DbType.Int64, id);
            _db.AddParameter(cmd, "Time", DbType.Int32, time);
            _db.AddParameter(cmd, "Year", DbType.Int32, year);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_Chart_MonthCompare(Int64 parentid, int time1, int time2, int year1, int year2, string code)
        {
            IDbCommand cmd = _db.CreateCommand("pr_Chart_MonthCompare", true);
            _db.AddParameter(cmd, "ParentID", DbType.Int64, parentid);
            _db.AddParameter(cmd, "Time1", DbType.Int32, time1);
            _db.AddParameter(cmd, "Time2", DbType.Int32, time2);
            _db.AddParameter(cmd, "Year1", DbType.Int32, year1);
            _db.AddParameter(cmd, "Year2", DbType.Int32, year2);
            _db.AddParameter(cmd, "Code", DbType.String, code);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }

        public DataTable pr_IndexMonth_SelectByIDAndTimeForQuy(int id, int time1, int time2, int year)
        {
            IDbCommand cmd = _db.CreateCommand("pr_IndexMonth_SelectByIDAndTimeForQuy", true);
            _db.AddParameter(cmd, "ID", DbType.Int32, id);
            _db.AddParameter(cmd, "Time1", DbType.Int32, time1);
            _db.AddParameter(cmd, "Time2", DbType.Int32, time2);
            _db.AddParameter(cmd, "Year", DbType.Int32, year);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }
        #endregion
    }
}