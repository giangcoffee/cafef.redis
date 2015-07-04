using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CafeF.Redis.TestUpdate.BCTC
{
    public class SqlDb
    {
        public static readonly string BCTCConnectionString = ConfigurationManager.ConnectionStrings["BCTC"].ConnectionString;
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FinanceChannelSlave"].ConnectionString;
        
        public SqlDb(){}

        public static DataTable GetBCTCSymbols()
        {
            var ret = new DataTable();
            using (var cnn = new SqlConnection(BCTCConnectionString))
            {
                cnn.Open();
                using(var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_CafeF_DataCrawler_GetStockData";
                    using(var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ret);
                    }
                }
                cnn.Close();
            }
            return ret;
        }
        public static DataTable GetBCTCValue(string symbol, string type, int year, int quarter)
        {
            var ret = new DataTable();
            using (var cnn = new SqlConnection(BCTCConnectionString))
            {
                cnn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_CafeF_DataCrawler_GetFinanceValue";
                    cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.NVarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, type));
                    cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, year));
                    cmd.Parameters.Add(new SqlParameter("@quarter", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, quarter));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ret);
                    }
                }
                cnn.Close();
            }
            return ret;
        }

        public static DataTable GetBondValue(string country, string type)
        {
            var ret = new DataTable();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CafeF_v2_GetBondData";
                    cmd.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, country));
                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, type));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ret);
                    }
                }
                cnn.Close();
            }
            return ret;
        }
        public static DataTable GetBondCountry(string code)
        {
            var ret = new DataTable();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CafeF_v2_GetBondCountry";
                    cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.NVarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ret);
                    }
                }
                cnn.Close();
            }
            return ret;
        }
    }
}
