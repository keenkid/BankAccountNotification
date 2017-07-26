using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.FileMonitor
{
    public interface IMonitorObserver
    {
        MonitorContent MonitorData { get; }

        ActionResult MonitorAction(string filename);
    }
}
