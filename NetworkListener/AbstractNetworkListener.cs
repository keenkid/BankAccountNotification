using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration;
using SunGard.AvantGard.Solution.Ban.Common.Logging;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener
{
    public abstract class AbstractNetworkListener : INetworkListener
    {
        protected static NetworkListenerSection _section = null;

        protected static ILog _logger = null;

        static AbstractNetworkListener()
        {
            _section = NetworkListenerSection.Instance;
            _logger = new Log4NetLogFactory().GetLog("transaction");
        }

        public abstract void Start();

        public abstract void Stop();

        protected string ReceiveMessage(ISocketMessage sm)
        {
            byte[] buffer = sm.ReceiveClientMessage();
            string message = _section.Environment.SystemEncode.GetString(buffer).TrimEnd('\0');
            _logger.InfoFormat("Message received from client");

            return message;
        }

        protected void ResponseMessage(ISocketMessage sm)
        {
            byte[] buffer = _section.Environment.SystemEncode.GetBytes(_section.Receipt.PositiveFeedback);
            sm.SendClientMessage(buffer);
            _logger.InfoFormat("Send feedback [{0}] to client success.", _section.Receipt.PositiveFeedback);
        }

        protected void ResponseMessage(ISocketMessage sm, bool receivedMessageValidation)
        {
            string feedbackMessage = string.Empty;
            if (receivedMessageValidation)
            {
                feedbackMessage = _section.Receipt.PositiveFeedback;
            }
            else
            {
                feedbackMessage = _section.Receipt.NegativeFeedback;
            }
            byte[] buffer = _section.Environment.SystemEncode.GetBytes(feedbackMessage);
            sm.SendClientMessage(buffer);
            _logger.InfoFormat("Send feedback [{0}] to client success.", feedbackMessage);
        }
    }
}
