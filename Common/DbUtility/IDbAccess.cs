using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public interface IDbAccess
    {
        List<string> GetOnlineAccounts();
    }
}
