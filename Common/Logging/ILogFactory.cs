using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common.Logging
{
    public interface ILogFactory
    {
        ILog GetLog(string name);
    }
}
