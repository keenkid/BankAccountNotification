using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class BodyElement : ConfigurationElement
    {
        private const string PROP_STRUCTNAME = "structName";

        private const string PROP_TABLENAME = "tableName";

        [ConfigurationProperty(PROP_STRUCTNAME, IsRequired = true)]
        public string StructName
        {
            get
            {
                return this[PROP_STRUCTNAME].ToString();
            }
        }

        [ConfigurationProperty(PROP_TABLENAME, IsRequired = true)]
        public string TableName
        {
            get
            {
                return this[PROP_TABLENAME].ToString();
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
