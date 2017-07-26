using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SunGardHistoryStatement
    {
        public SunGardMessageHeader Header { get; set; }

        public List<SunGardHistoryStatementBody> Body { get; set; }
    }
}
