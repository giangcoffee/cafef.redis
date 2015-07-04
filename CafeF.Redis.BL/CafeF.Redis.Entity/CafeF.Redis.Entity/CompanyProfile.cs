using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region CompanyProfile Models
    [Serializable]
    //key format: "CompanyProfile:{0}:Symbol" / "companyprofileid:{0}:Object"
    public class CompanyProfile //Thông tin công ty - Dùng trong trang Stock
    {
        public CompanyProfile()
        {
            financeInfos = new List<FinanceInfo>();
            FinancePeriods = new List<FinancePeriod>();
            commonInfos = new CommonInfo();
            Leaders = new List<Leader>();
            MajorOwners = new List<MajorOwner>();
            Subsidiaries = new List<OtherCompany>();
            AssociatedCompanies = new List<OtherCompany>();
            basicInfos = new BasicInfo();
            AssociatedCeo = new List<StockCeo>();
        }

        //public string Key { get; set; } // key của object này là Symbol
        public string Symbol { get; set; }
        /// <summary>
        /// Thông tin tài chính
        /// </summary>
        public List<FinanceInfo> financeInfos { get; set; }
        /// <summary>
        /// Niên độ kế toán
        /// </summary>
        public List<FinancePeriod> FinancePeriods { get; set; }
        /// <summary>
        /// Thông tin chung, trong Box thông tin công ty
        /// </summary>
        public CommonInfo commonInfos { get; set; }
        /// <summary>
        /// Ban lãnh đạo
        /// </summary>
        public List<Leader> Leaders { get; set; }
        /// <summary>
        /// Cổ đông lớn
        /// </summary>
        public List<MajorOwner> MajorOwners { get; set; }
        /// <summary>
        /// Công ty con
        /// </summary>
        public List<OtherCompany> Subsidiaries { get; set; }
        /// <summary>
        /// Công ty liên kết
        /// </summary>
        public List<OtherCompany> AssociatedCompanies { get; set; }

        public BasicInfo basicInfos { get; set; }

        public List<StockCeo> AssociatedCeo { get; set; }
    }

    #endregion

}