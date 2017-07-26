using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    public class BcomAmount : IFieldFormat
    {
        public string Name
        {
            get { return "bcomamount"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            decimal amount;
            string amountString = xdoc.Descendants(source).FirstOrDefault().Value;
            string cdFlag = xdoc.Descendants(parameters).FirstOrDefault().Value;

            if (!decimal.TryParse(amountString, out amount))
            {
                return amountString;
            }

            if (string.IsNullOrEmpty(cdFlag))
            {
                return amountString;
            }

            if (string.IsNullOrEmpty(cdFlag))
            {
                return amountString;
            }

            if (string.Equals("C", cdFlag, StringComparison.CurrentCultureIgnoreCase))
            {
                return amountString;
            }
            else if (string.Equals("D", cdFlag, StringComparison.CurrentCultureIgnoreCase))
            {
                return (amount * -1).ToString("0.00");
            }

            return amountString;
        }
    }
}
