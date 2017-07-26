using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener
{
    public interface ISocketMessage
    {
        int SendTimeOut { get; set; }

        int ReceiveTimeOut { get; set; }

        void SendClientMessage(byte[] message);

        byte[] ReceiveClientMessage();
    }
}
