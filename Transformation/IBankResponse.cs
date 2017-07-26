using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common.Logging;
using SunGard.AvantGard.Solution.Ban.BizBase;

namespace SunGard.AvantGard.Solution.Ban.Transformation
{
    public interface IBankResponse
    {
        string BankMessage { get; set; }

        ILog Logger { get; set; }

        TransactionBrief TransactionBrief { get; set; }

        void Handle();
    }
}
