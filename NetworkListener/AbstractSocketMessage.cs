using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener
{
    public abstract class AbstractSocketMessage : ISocketMessage
    {
        protected virtual int BufferSize
        {
            get
            {
                return 1024;
            }
        }

        public abstract void SendClientMessage(byte[] message);

        public abstract byte[] ReceiveClientMessage();

        public int SendTimeOut
        {
            get;
            set;
        }

        public int ReceiveTimeOut
        {
            get;
            set;
        }
    }
}
