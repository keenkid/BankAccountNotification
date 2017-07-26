using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    class IcbcCode2Currency : IFieldFormat
    {
        public string Name
        {
            get { return "icbccode2cur"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            return "CNY";
        }
    }
}
