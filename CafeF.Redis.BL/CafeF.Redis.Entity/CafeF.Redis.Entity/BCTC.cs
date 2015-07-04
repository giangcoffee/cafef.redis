using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Entity
{
    public class BCTC
    {
        public BCTC(){ Values = new List<BCTCValue>();}

        public string Symbol { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }

        public List<BCTCValue> Values { get; set; }

    }

    public class BCTCValue
    {
        public BCTCValue(){}

        public string Code { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
