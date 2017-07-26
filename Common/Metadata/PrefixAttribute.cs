using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class PrefixAttribute : Attribute
    {
        private string _prefix = string.Empty;

        public PrefixAttribute(string prefix)
        {
            _prefix = prefix;
        }

        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                _prefix = value;
            }
        }
    }
}
