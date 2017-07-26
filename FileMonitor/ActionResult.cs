using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.FileMonitor
{
    public class ActionResult
    {
        public string ActionSender { get; set; }

        public string SourceFileName { get; set; }

        public string Backup2Folder { get; set; }

        public ResultCode Code { get; set; }

        public Exception AppException { get; set; }
    }

    public enum ResultCode
    {
        S,
        F,
        R
    }
}
