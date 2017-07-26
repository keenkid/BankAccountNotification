using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.BizBase;
using SunGard.AvantGard.Solution.Ban.Common;

namespace SunGard.AvantGard.Solution.Ban.ABC
{
    class AbcMessageConvert : IMessageConvert
    {
        private static AbcFieldSection __section = null;

        private string _message = string.Empty;

        static AbcMessageConvert()
        {
            if (null == __section)
            {
                __section = AbcFieldSection.Instance;
            }
        }

        public AbcMessageConvert(string source)
        {
            _message = source;
        }

        public string Convert()
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("<fakeroot>");

                foreach (FieldElement elmt in __section.Fields)
                {
                    builder.AppendFormat("<{0}>{1}</{0}>", elmt.Name, _message.GbkSubString(elmt.Start, elmt.End - elmt.Start + 1));
                }

                builder.Append("</fakeroot>");

                return builder.ToString();
            }
            catch
            {
                throw new Exception("Convert abc message exception");
            }
        }
    }
}
