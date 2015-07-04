using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using CafeF.Redis.UpdateService;
using ServiceStack.Redis;

namespace CafeF.Redis.TestUpdate.BCTC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var ls = GetTopBCTC("SSI", "BSheet", 2011, 1, 4);
            var log = new LogUtils(LogType.TextLog, true, AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\");
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

            var symbols = SqlDb.GetBCTCSymbols();
            textBox1.Text = "Begin : " + DateTime.Now.ToString() + Environment.NewLine;
            var types = new List<string>() {"IncSta", "BSheet", "CashFlow", "CashFlowDirect"};
            foreach (DataRow dr in symbols.Rows)
            {
                var symbol = dr["Symbol"].ToString();
                //if(symbol!="SSI") continue;
                textBox1.Text += symbol + ",";
               log.WriteEntry(symbol + ",", EventLogEntryType.Warning);
                for(var year = 2005; year <= 2011; year++)
                {
                    for(var quarter = 0; quarter<=4; quarter++)
                    {
                        if(year==2011 && quarter!= 1) continue;
                        foreach (var type in types)
                        {
                            var o = GetBCTC(symbol, type, year, quarter);
                            if(o==null) continue;
                            var key = string.Format(RedisKey.BCTCKey, symbol.ToUpper(), type.ToUpper(), quarter == 0 ? 0 : 1, year, quarter);
                            if (redis.ContainsKey(key))
                                redis.Set(key, o);
                            else
                                redis.Add(key, o);
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            textBox1.Text += "End : " + DateTime.Now.ToString() + Environment.NewLine;
            
        }
        private List<Entity.BCTC> GetTopBCTC(string symbol, string type, int fromYear, int fromQuarter, int count)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var keys = redis.SearchKeys(string.Format(RedisKey.BCTCKey, symbol.Trim().ToUpper(), type.Trim().ToUpper(), fromQuarter > 0 ? 1 : 0, "*", "*"));
            keys.Sort();
            keys.Reverse();
            var i = 0;
            while (i < keys.Count)
            {
                if (i >= count) { keys.RemoveAt(i); continue; }
                var tmp = keys[i].Substring(keys[i].Length - 6);
                var year = int.Parse(tmp.Substring(0, 4));
                var quarter = int.Parse(tmp.Substring(5));
                if (year > fromYear) { keys.RemoveAt(i); continue; }
                if (year == fromYear && quarter > fromQuarter) { keys.RemoveAt(i); continue; }
                i++;
            }
            return keys.Count == 0 ? new List<Entity.BCTC>() : redis.GetAll<Entity.BCTC>(keys).Values.ToList();
        }
        private Redis.Entity.BCTC GetBCTC(string symbol, string type, int year, int quarter)
        {
            var dt = SqlDb.GetBCTCValue(symbol, type, year, quarter);
            var bHaveData = false;
            foreach (DataRow dr in dt.Rows)
            {
                if(dr["ChiTieuValue"].ToString()!="-1") {
                    bHaveData = true; break;}
            }
            if (!bHaveData) return null;
            var ret = new Entity.BCTC() {Symbol = symbol, Type = type, Quarter = quarter, Year = year};
            var ls = new List<BCTCValue>();
            foreach (DataRow dr in dt.Rows)
            {
               ls.Add(new BCTCValue(){Code = dr["MappingCode"].ToString(), Name = dr["MappingName"].ToString(), Value = double.Parse(dr["ChitieuValue"].ToString())});
            }
            ret.Values = ls;
            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

            var test = redis.Get<Bond>(string.Format(RedisKey.BondKey, "Bồ Đào Nha", "3"));

            var countries = SqlDb.GetBondCountry("A");
            var types = new List<string>() {"1", "3", "5", "10"};
            foreach (DataRow dr in countries.Rows)
            {
                var country = dr["CountryName"].ToString();
                foreach (var type in types)
                {
                    var dt = SqlDb.GetBondValue(country, type);
                    if (dt.Rows.Count == 0) continue;
                    var o = new Bond() {BondCode = dt.Rows[0]["BondCode"].ToString(), BondCountry = dt.Rows[0]["CountryName"].ToString(), BondType = dt.Rows[0]["BondType"].ToString(), BondEnName = dt.Rows[0]["ENName"].ToString(), BondVnName = dt.Rows[0]["VNName"].ToString()};
                    var values = new List<BondValue>();
                    foreach (DataRow value in dt.Rows)
                    {
                        values.Add(new BondValue(){TradeDate = (DateTime)value["TradeDate"], ClosePrice = double.Parse(value["ClosePrice"].ToString())});
                    }
                    o.BondValues = values;
                    var key = string.Format(RedisKey.BondKey, country, type);
                    if (redis.ContainsKey(key))
                        redis.Set(key, o);
                    else
                        redis.Add(key, o);
                }
            }
        }

    }
}
