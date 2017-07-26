using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SunGardHistoryStatementBody
    {
        public string RefID { get; set; }

        public string BankRefID { get; set; }

        public string TransDate { get; set; }

        public string TransTime { get; set; }

        public string AcctNo { get; set; }

        public string AcctName { get; set; }

        public string Amount { get; set; }

        public string Currency { get; set; }

        public string Balance { get; set; }

        public string CptyAcctNo { get; set; }

        public string CptyAcctName { get; set; }

        public string CptyBankName { get; set; }

        public string Remark { get; set; }

        public string Abstract { get; set; }
    }
}
