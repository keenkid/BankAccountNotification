using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class VariantElement : ConfigurationElement
    {
        private const string PROP_DESC = "desc";

        private const string PROP_TAG = "tag";

        private const string PROP_NAME = "name";

        private const string PROP_TYPE = "type";

        [ConfigurationProperty(PROP_DESC, IsRequired = false)]
        public string Description
        {
            get
            {
                return this[PROP_DESC].ToString();
            }
        }

        [ConfigurationProperty(PROP_TAG, IsKey = true, IsRequired = true)]
        public string Tag
        {
            get
            {
                return this[PROP_TAG].ToString();
            }
        }

        [ConfigurationProperty(PROP_NAME, IsRequired = true)]
        public string Name
        {
            get
            {
                return this[PROP_NAME].ToString();
            }
        }

        [ConfigurationProperty(PROP_TYPE, IsRequired = true)]
        public string FieldFormat
        {
            get
            {
                return this[PROP_TYPE].ToString();
            }
        }
    }
}
