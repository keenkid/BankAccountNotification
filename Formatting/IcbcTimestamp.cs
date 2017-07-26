using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    class IcbcTimestamp : IFieldFormat
    {
        public string Name
        {
            get { return "icbctimestamp"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            try
            {
                string srcVal = xdoc.Descendants(source).FirstOrDefault().Value;
                srcVal = srcVal.Replace("-", string.Empty).Replace(".", string.Empty);
                return srcVal;
            }
            catch
            {
                return DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            }
        }
    }
}
