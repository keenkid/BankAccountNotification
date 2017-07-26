using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration
{
    public class ReceiptElement : ConfigurationElement
    {
        private const string PROP_POSITIVE = "positive";

        private const string PROP_NEGATIVE = "negative";

        private const string PROP_INVALID = "invalid";

        [ConfigurationProperty(PROP_POSITIVE, IsRequired = true)]
        public string PositiveFeedback
        {
            get
            {
                return this[PROP_POSITIVE].ToString();
            }
        }

        [ConfigurationProperty(PROP_NEGATIVE, IsRequired = true)]
        public string NegativeFeedback
        {
            get
            {
                return this[PROP_NEGATIVE].ToString();
            }
        }

        [ConfigurationProperty(PROP_INVALID, IsRequired = true)]
        public string InvalidFeedback
        {
            get
            {
                return this[PROP_INVALID].ToString();
            }
        }
    }
}
