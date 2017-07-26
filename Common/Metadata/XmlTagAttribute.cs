using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XmlTagAttribute : Attribute
    {
        public XmlTagAttribute()
        {

        }

        public string Tag { get; set; }
    }
}
