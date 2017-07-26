using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public interface IBankTransactionMap
    {
        List<TransactionBrief> BankTransactions { get; }

        TransactionBrief QueryTransaction(string bankCode, string transCode);
    }
}
