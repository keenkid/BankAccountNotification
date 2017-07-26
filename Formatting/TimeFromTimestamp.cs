using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    class TimeFromTimestamp : IFieldFormat
    {
        public string Name
        {
            get { return "timeFromTimeStamp"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            var elmt = xdoc.Element(source);
            if (null == elmt)
            {
                return string.Empty;
            }
            else
            {
                string timestamp = elmt.Value.Trim();
                string time = timestamp.Substring(11, 8);
                return time.Replace(".", string.Empty);
            }
        }
    }
}
