using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    public class BocAmount : IFieldFormat
    {
        public string Name
        {
            get { return "bocamount"; }
        }

        public string Convert(System.Xml.Linq.XDocument xdoc, string source, string parameters)
        {
            try
            {
                var val = xdoc.Descendants(source).FirstOrDefault().Value;
                if (string.IsNullOrEmpty(val) || string.IsNullOrEmpty(parameters))
                {
                    return string.Empty;
                }
                var reversal = xdoc.Descendants(parameters).FirstOrDefault().Value;
                if (!string.IsNullOrEmpty(reversal) && ("1" == reversal.ToUpper()))
                {
                    if (val.StartsWith("-"))
                    {
                        return val.Substring(1);
                    }
                    else
                    {
                        return string.Format("-{0}", val);
                    }
                }
                return source;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
