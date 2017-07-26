using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.FileMonitor
{
    public class CustomizeTimer : System.Timers.Timer
    {
        public CustomizeTimer() : base() { }

        public CustomizeTimer(int interval) : base(interval) { }

        public string Tag { get; set; }
    }
}
