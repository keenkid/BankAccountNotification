using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using SunGard.AvantGard.Solution.Ban.Common;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration
{
    public class EnvironmentElement : ConfigurationElement
    {
        private const string PROP_READTIMEOUT = "readTimeout";

        private const string PROP_WRITETIMEOUT = "writeTimeout";

        private const string PROP_ENCODING = "encoding";

        [ConfigurationProperty(PROP_READTIMEOUT, IsRequired = false)]
        public int ReadTimeout
        {
            get
            {
                int timeout = 60;
                if (int.TryParse(this[PROP_READTIMEOUT].ToString(), out timeout))
                {
                    return timeout;
                }
                return timeout;
            }
        }

        [ConfigurationProperty(PROP_WRITETIMEOUT, IsRequired = false)]
        public int WriteTimeout
        {
            get
            {
                int timeout = 60;
                if (int.TryParse(this[PROP_WRITETIMEOUT].ToString(), out timeout))
                {
                    return timeout;
                }
                return timeout;
            }
        }

        [ConfigurationProperty(PROP_ENCODING, IsRequired = true)]
        [TypeConverter(typeof(EncodingTypeConverter))]
        public Encoding SystemEncode
        {
            get
            {
                try
                {
                    return (Encoding)this[PROP_ENCODING];
                }
                catch
                {
                    return Encoding.UTF8;
                }
            }
        }
    }
}
