using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common;
using SunGard.AvantGard.Solution.Ban.BizBase;
using System.Xml.Linq;
using System.Reflection;
using SunGard.AvantGard.Solution.Ban.Transformation;

namespace SunGard.AvantGard.Solution.Ban.ABC
{
    [Prefix("BN")]
    [TransCode("1022")]
    public class BankAccountNotification : AbstractBanResponse
    {
        protected override void PreParsePacket()
        {
            IMessageConvert mc = new AbcMessageConvert(BankMessage);

            BankMessage = mc.Convert();
        }
    }
}
