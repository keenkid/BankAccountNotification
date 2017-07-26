using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public abstract class SunGardMessage
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            PropertyInfo[] props = this.GetType().GetProperties();

            props.ToList().ForEach(p => 
            {
                var xmlTag = p.GetCustomAttributes(typeof(XmlTagAttribute), false).FirstOrDefault() as XmlTagAttribute;
                builder.Append(string.Format("<{0}>{1}</{0}>", xmlTag.Tag, p.GetValue(this, null)));
            });

            return builder.ToString();
        }
    }
}
