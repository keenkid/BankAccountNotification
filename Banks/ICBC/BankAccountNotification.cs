using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common;
using SunGard.AvantGard.Solution.Ban.Transformation;

namespace SunGard.AvantGard.Solution.Ban.ICBC
{
    [Prefix("BN")]
    [TransCode("1022")]
    public class BankAccountNotification : AbstractBanResponse
    {
        protected override bool SaveAsFile
        {
            get
            {
                return true;
            }
        }
    }
}
