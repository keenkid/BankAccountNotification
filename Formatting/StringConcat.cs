using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    public class StringConcat : IFieldFormat
    {
        public string Name
        {
            get { return "StringConcat"; }
        }

        public string Convert(XDocument xdoc, string source, string parameters)
        {
            try
            {
                var @params = parameters.Split('|');

                StringBuilder builder = new StringBuilder();

                foreach (var item in @params)
                {
                    builder.Append(xdoc.Descendants(item).FirstOrDefault().Value);
                }
                return builder.ToString();
            }
            catch
            {
                return DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            }
        }
    }
}
