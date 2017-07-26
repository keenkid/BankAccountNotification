using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public interface IBanData
    {
        bool ClientValidation(string ipAddress);

        bool ClientValidation(EndPoint ep);

        string QueryClientAddenda(string ipAddress);

        string QueryClientAddenda(EndPoint ep);
    }
}
