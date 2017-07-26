using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class VariantElement : ConfigurationElement
    {
        private const string PROP_DESC = "desc";

        private const string PROP_TAG = "tag";

        private const string PROP_RFCDATA = "rfcData";

        [ConfigurationProperty(PROP_DESC, IsKey = false, IsRequired = true)]
        public string Desc
        {
            get
            {
                return this[PROP_DESC].ToString();
            }
        }

        [ConfigurationProperty(PROP_TAG, IsKey = false, IsRequired = true)]
        public string Tag
        {
            get
            {
                return this[PROP_TAG].ToString();
            }
        }

        [ConfigurationProperty(PROP_RFCDATA, IsKey = true, IsRequired = true)]
        public string RfcData
        {
            get
            {
                return this[PROP_RFCDATA].ToString();
            }
        }
    }
}
