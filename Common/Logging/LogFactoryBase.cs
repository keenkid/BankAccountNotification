using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common.Logging
{
    public abstract class LogFactoryBase : ILogFactory
    {
        protected string ConfigFile
        {
            get;
            private set;
        }

        protected LogFactoryBase(string configFile)
        {
            if (Path.IsPathRooted(configFile))
            {
                ConfigFile = configFile;
                return;
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile);

            if (File.Exists(filePath))
            {
                ConfigFile = filePath;
                return;
            }
            else
            {
                throw new FileNotFoundException(configFile);
            }
        }

        public abstract ILog GetLog(string name);
    }
}
