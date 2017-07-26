using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.ComponentModel;
using SunGard.AvantGard.Solution.Ban.Common;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration
{
    public class TerminateElement : ConfigurationElement
    {
        private const string PROP_IP = "ip";

        private const string PROP_PORT = "port";

        [ConfigurationProperty(PROP_IP, IsRequired = true)]
        [TypeConverter(typeof(IPAddressTypeConverter))]
        public IPAddress ListenIP
        {
            get
            {
                return (IPAddress)this[PROP_IP];
            }
        }

        [ConfigurationProperty(PROP_PORT, IsRequired = true)]
        public int ListenPort
        {
            get
            {
                int port = 9527;
                if (int.TryParse(this[PROP_PORT].ToString(), out port))
                {
                    return port;
                }
                return port;
            }
        }
    }
}
