using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class SystemElement : ConfigurationElement
    {
        private const string PROP_FOLDER = "folder";

        private const string PROP_INTERVAL = "interval";

        private const string PROP_TIMEOUT = "rfctimeout";

        [ConfigurationProperty(PROP_FOLDER, IsRequired = false)]
        public string MonitorPath
        {
            get
            {
                string folder = this[PROP_FOLDER].ToString();
                string dir = Directory.GetParent(typeof(SystemElement).Assembly.Location).FullName;
                string fullPath = Path.Combine(dir, folder);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                return fullPath;
            }
        }

        [ConfigurationProperty(PROP_INTERVAL, IsRequired = true)]
        public int Interval
        {
            get
            {
                int interval = 60;
                if (int.TryParse(this[PROP_INTERVAL].ToString(), out interval))
                {
                    return interval;
                }
                return interval;
            }
        }

        [ConfigurationProperty(PROP_TIMEOUT, IsRequired = false)]
        public int RfcConnectionTimeout
        {
            get
            {
                int timeout = 30;
                if (int.TryParse(this[PROP_TIMEOUT].ToString(), out timeout))
                {
                    return timeout;
                }
                return timeout;
            }
        }
    }
}
