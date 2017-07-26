using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    class IcbcAmount : IFieldFormat
    {
        public string Name
        {
            get { return "icbcamount"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            try
            {
                string srcVal = xdoc.Descendants(source).FirstOrDefault().Value;
                decimal amount = 0;
                if (!decimal.TryParse(srcVal, out amount))
                {
                    amount = 0;
                }
                amount = amount / 100;
                string dcFlag = xdoc.Descendants(parameters).FirstOrDefault().Value;
                if ("D" == dcFlag)
                {
                    amount = -1 * amount;
                }
                return amount.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
