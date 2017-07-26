using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration
{
    public class NetworkListenerSection : ConfigurationSection
    {
        private const string ELMT_TERMINATE = "terminate";

        private const string ELMT_ENVIRONMENT = "environment";

        private const string ELMT_RECEIPT = "receipt";

        private const string SECTION_NAME = "NetworkListenerSection";

        [ConfigurationProperty(ELMT_ENVIRONMENT, IsRequired = false)]
        public EnvironmentElement Environment
        {
            get
            {
                return this[ELMT_ENVIRONMENT] as EnvironmentElement;
            }
        }

        [ConfigurationProperty(ELMT_TERMINATE, IsRequired = true)]
        public TerminateElement Terminate
        {
            get
            {
                return this[ELMT_TERMINATE] as TerminateElement;
            }
        }

        [ConfigurationProperty(ELMT_RECEIPT, IsRequired = false)]
        public ReceiptElement Receipt
        {
            get
            {
                return this[ELMT_RECEIPT] as ReceiptElement;
            }
        }

        private static object obj = new object();

        private static NetworkListenerSection __instance = null;

        protected NetworkListenerSection()
        {

        }

        public static NetworkListenerSection Instance
        {
            get
            {
                lock (obj)
                {
                    if (null == __instance)
                    {
                        var asmLocation = typeof(NetworkListenerSection).Assembly.Location;
                        var config = ConfigurationManager.OpenExeConfiguration(asmLocation);
                        __instance = config.GetSection(SECTION_NAME) as NetworkListenerSection;
                    }
                    return __instance;
                }
            }
        }
    }
}
