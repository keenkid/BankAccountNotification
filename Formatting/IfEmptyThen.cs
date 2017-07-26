using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    class IfEmptyThen:IFieldFormat
    {
        public string Name
        {
            get { return "ifemptythen"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(source))
                {
                    return parameters;
                }

                string srcVal = xdoc.Descendants(source).FirstOrDefault().Value;
                if (string.IsNullOrEmpty(srcVal))
                {
                    srcVal = xdoc.Descendants(parameters).FirstOrDefault().Value;
                }
                return srcVal;
            }
            catch
            {
                return parameters;
            }
        }
    }
}
