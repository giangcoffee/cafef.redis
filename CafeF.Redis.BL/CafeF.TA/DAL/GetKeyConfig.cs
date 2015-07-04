using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CafeF.TA.DAL
{
    public class GetKeyConfig
    {
        public static string AllowSqlCache = ConfigurationManager.AppSettings.Get("AllowSqlCache").ToString().Trim();
        public static string AllowDistCache = ConfigurationManager.AppSettings.Get("AllowDistCache").ToString().Trim();

    }
}
