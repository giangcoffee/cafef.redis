using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CafeF.Redis.TestUpdate.Service
{
    public class SqlDb
    {
        private static readonly string NewsSlaveData = ConfigurationManager.ConnectionStrings["NewsSlave"].ConnectionString;
        private static readonly string SlaveData = ConfigurationManager.ConnectionStrings["FinanceChannelSlave"].ConnectionString;
        private static readonly string FinanceData = ConfigurationManager.ConnectionStrings["FinanceChannel"].ConnectionString;
        private static readonly string PriceData = ConfigurationManager.ConnectionStrings["PriceData"].ConnectionString;
        private static readonly string CrawlerData = ConfigurationManager.ConnectionStrings["CrawlerData"].ToString();

        private SqlConnection slaveConn;
        public SqlDb()
        {
            slaveConn = new SqlConnection(SlaveData);
        }

        public void OpenDb()
        {
            if (slaveConn.State != ConnectionState.Open) slaveConn.Open();
        }

        public void CloseDb()
        {
            if (slaveConn.State != ConnectionState.Closed) slaveConn.Close();
        }

        #region Symbol Data
        public DataTable GetSymbolData(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_SymbolData";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetChildrenCompany(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetChildren";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetFinanceData(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetFinanceData";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetChiTieuFinance(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetChiTieuFinance";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetFinancePeriod(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetFinancePeriod";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeos(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeos";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetShareHolders(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetShareHolders";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetDividendHistory(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetDividendHistory";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetAnalysisReports(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetAnalysisReports";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetAnalysisReports(string symbol, int top)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetAnalysisReports";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetAnalysisReports(string symbol, string date)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetAnalysisReportsByDate";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetSameCateCompanies(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetSameCateCompanies";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetSameEPSCompanies(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetSameEPSCompanies";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetSamePECompanies(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetSamePECompanies";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetPriceHistory(string symbol, int top)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandTimeout = 120;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetPriceHistory";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        /// <summary>
        /// Lấy lịch sử giá theo ngày
        /// </summary>
        /// <param name="symbol">Mã</param>
        /// <param name="date">Ngày (định dạng yyyy.MM.dd)</param>
        /// <returns></returns>
        public DataTable GetPriceHistory(string symbol, string date)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandTimeout = 120;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetPriceHistoryByDate";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetOrderHistory(string symbol, int top)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetOrderHistory";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetOrderHistory(string symbol, string date)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetOrderHistoryByDate";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetForeignHistory(string symbol, int top)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetForeignHistory";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetForeignHistory(string symbol, string date)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetForeignHistoryByDate";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetInternalHistory(string symbol, int top)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetInternalHistory";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCompanyNews(string symbol, int top)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCompanyNews";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCompanyNews(string symbol, string date)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCompanyNewsByDate";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, date));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetPriceData(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetPriceData";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCenterId(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCenterId";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }

        public DataTable GetSymbolList(int centerId)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockList";
                cmd.Parameters.Add(new SqlParameter("@centerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, centerId));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetTopStock()
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetTopStock";
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetPriceData(int tradeCenterId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SlaveData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetLastPriceData";
                    cmd.Parameters.Add(new SqlParameter("@centerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, tradeCenterId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public DataTable GetLastImportPriceDate(int tradeCenterId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SlaveData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetLastImportPriceDate";
                    cmd.Parameters.Add(new SqlParameter("@center", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, tradeCenterId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public string GetKbyFolder()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SlaveData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetKbyFolder";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt.Rows.Count > 0 ? dt.Rows[0]["CurrentFolder"].ToString() : "";
        }

       
        
        #endregion

        #region CEO
        public DataTable GetAllCeos()
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "SELECT DISTINCT CeoCode FROM tblCeoProfile";
                //cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.Text;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_Profile(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_Profile";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_Position(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_Position";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_Asset(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_Asset";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_Relation(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_Relation";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_School(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_School";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_Process(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_Process";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetCeosNew_News(string code)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetStockPage_GetCeosNew_News";
                cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, code));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }

        public DataTable GetCeosNew_NewsDetail(string ids)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(NewsSlaveData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetCeosNew_NewsDetail";
                    cmd.Parameters.Add(new SqlParameter("@ids", SqlDbType.VarChar, 4000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ids));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }

        public DataTable GetLichSuKien(string date)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetLichSuKien";
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.NVarChar, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetLichSuKienTomTat()
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "CafeF_v2_GetLichSuKienTomTat";
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        #endregion

        #region Land Project

        public DataTable GetAllLandProjects()
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "SELECT DISTINCT MaTienDo FROM tblTienDoBDS";
                //cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.Text;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetLandProject(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "Cafef_v2_GetStockPage_GetLandProject";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetLandProject_Area(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "Cafef_v2_GetStockPage_GetLandProject_Area";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        public DataTable GetLandProject_Profit(string symbol)
        {
            var dt = new DataTable();
            //using (var conn = new SqlConnection(SlaveData))
            //{
            //    conn.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = slaveConn;
                cmd.CommandText = "Cafef_v2_GetStockPage_GetLandProject_Profit";
                cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dap = new SqlDataAdapter(cmd))
                {
                    dap.Fill(dt);
                }
            }
            //    conn.Close();
            //}
            return dt;
        }
        #endregion

        #region Price Data
        public DataTable GetLastRealtimePriceDate(int tradeCenterId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(PriceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_GetLastRealtimePriceDate";
                    cmd.Parameters.Add(new SqlParameter("@center", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, tradeCenterId));

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public DataTable GetRealtimePrice(int tradeCenterId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(PriceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_GetPriceDataWithCenter";
                    cmd.Parameters.Add(new SqlParameter("@center", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, tradeCenterId));

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public DataTable GetChangedPriceSymbols(int tradeCenterId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(PriceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_GetChangedPriceData";
                    cmd.Parameters.Add(new SqlParameter("@center", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, tradeCenterId));
                    cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, "cafef"));

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }


        #endregion

        #region Update Service
        public DataTable GetStockUpdate()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(FinanceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetStockUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public void UpdateStockMonitor(string ids)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(FinanceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_UpdateStockMonitor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ids", SqlDbType.VarChar, 4000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ids));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        #endregion

        #region Crawler Data
        public static DataTable GetCrawlerData()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(CrawlerData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM GiaCafeF";
                    cmd.CommandType = CommandType.Text;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public static DataTable GetManualProductData()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SlaveData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetManualProductData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        #endregion

        #region Fund Transaction
        public DataTable GetFundHistory(string symbol, int top)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(PriceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetStockPage_GetFundHistory";
                    cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                    cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        public static DataTable GetFundUpdate()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(PriceData))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CafeF_v2_GetStockPage_GetFundUpdate";
                    //cmd.Parameters.Add(new SqlParameter("@symbol", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, symbol));
                    //cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, top));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
        #endregion
    }
}
