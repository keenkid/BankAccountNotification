using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    class RemoveChar : IFieldFormat
    {
        public string Name
        {
            get { return "removechar"; }
        }

        public string Convert(System.Xml.Linq.XDocument xdoc, string source, string parameters)
        {
            var elmt = xdoc.Descendants(source).FirstOrDefault();
            if (null == elmt)
            {
                return string.Empty;
            }
            else
            {
                string rslt = elmt.Value.Trim();
                return rslt.Replace(parameters, string.Empty);
            }
        }
    }
}
