using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CafeF.DLVM.DAL
{
    public class StoredProceduresMaster
    {
        private MainDBMaster _db;

        public StoredProceduresMaster(MainDBMaster db)
        {
            _db = db;
        }

        public MainDBMaster Database
        {
            get { return _db; }
        }

        protected DataTable CreateDataTable(IDbCommand command)
        {
            return _db.CreateDataTable(command);
        }

        public void home_pr_Project_UpdateCount(string code)
        {
            IDbCommand cmd = _db.CreateCommand("home_pr_Project_UpdateCount", true);
            _db.AddParameter(cmd, "PCode", DbType.String, code);

            CreateDataTable(cmd);
        }
    }
}
