using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CafeF.Redis.KhopLenh
{
    public class SqlDb
    {
        private static readonly string SessionPriceData = ConfigurationManager.ConnectionStrings["PriceMaster"].ConnectionString;
       
        public SqlDb()
        {
        }

        public static DataTable UpdateData(string xml)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SessionPriceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_UpdatePriceData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@xml", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, xml));
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
    }
}
