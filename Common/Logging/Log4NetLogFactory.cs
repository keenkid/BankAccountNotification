using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common.Logging
{
    public class Log4NetLogFactory : LogFactoryBase
    {
        public Log4NetLogFactory()
            : this("log4net.xml")
        {
        }

        public Log4NetLogFactory(string log4netXml)
            : base(log4netXml)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ConfigFile));
        }

        public override ILog GetLog(string name)
        {
            return new Log4NetLog(log4net.LogManager.GetLogger(name));
        }
    }
}
