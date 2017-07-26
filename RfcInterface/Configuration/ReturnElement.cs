using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class ReturnElement : ConfigurationElement
    {
        private const string PROP_CODE = "code";

        private const string PROP_MESSAGE = "message";

        [ConfigurationProperty(PROP_CODE, IsRequired = true)]
        public string ReturnCode
        {
            get
            {
                return this[PROP_CODE].ToString();
            }
        }

        [ConfigurationProperty(PROP_MESSAGE, IsRequired = true)]
        public string ReturnMessage
        {
            get
            {
                return this[PROP_MESSAGE].ToString();
            }
        }
    }
}
