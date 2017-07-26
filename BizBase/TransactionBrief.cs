using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class TransactionBrief
    {
        public string BankCode { get; set; }

        public string TransCode { get; set; }

        public string TransPrefix { get; set; }

        public Type TransactionType { get; set; }
    }
}
