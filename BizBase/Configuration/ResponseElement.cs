using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class ResponseElement : ConfigurationElement
    {
        private const string PROP_PREFIX = "prefix";

        private const string PROP_MULTI = "multiTag";

        [ConfigurationProperty(PROP_PREFIX, IsRequired = true)]
        public string Prefix
        {
            get
            {
                return this[PROP_PREFIX].ToString();
            }
        }

        [ConfigurationProperty(PROP_MULTI, IsRequired = true)]
        public string MultiTag
        {
            get
            {
                return this[PROP_MULTI].ToString();
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public VariantElementCollection Variants
        {
            get
            {
                return this[""] as VariantElementCollection;
            }
        }
    }
}
