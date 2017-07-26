using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class HeaderElement : ConfigurationElement
    {
        private const string PROP_NAME = "structName";

        [ConfigurationProperty(PROP_NAME, IsRequired = true)]
        public string StructName
        {
            get
            {
                return this[PROP_NAME].ToString();
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
        public VariantElementCollection Items
        {
            get
            {
                return this[""] as VariantElementCollection;
            }
        }
    }
}
