using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    public class FinanceReport
    {
        public FinanceReport(){}

        /// <summary>
        /// Mã cổ phiếu
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// DS Báo cáo tài chính
        /// </summary>
        public string HtmlContent { get; set; }
    }
}
