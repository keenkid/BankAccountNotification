using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener
{
    public class TcpClientMessage : AbstractSocketMessage
    {
        private TcpClient _client = null;

        private NetworkStream _networkStream = null;

        public TcpClientMessage(TcpClient client, EnvironmentElement env)
        {
            _client = client;
            _networkStream = _client.GetStream();
            ReceiveTimeOut = env.ReadTimeout * 1000;
            SendTimeOut = env.WriteTimeout * 1000;
        }

        public override void SendClientMessage(byte[] message)
        {
            try
            {
                lock (_networkStream)
                {
                    _networkStream.WriteTimeout = SendTimeOut;
                    _networkStream.Write(message, 0, message.Length);
                }
            }
            catch
            {
                throw;
            }
        }

        public override byte[] ReceiveClientMessage()
        {
            MemoryStream memoryStream = new MemoryStream(BufferSize);

            int bytesReadCount = 0;
            byte[] buffer = new byte[BufferSize];

            try
            {
                lock (_networkStream)
                {
                    _networkStream.ReadTimeout = ReceiveTimeOut;
                    do
                    {
                        bytesReadCount = _networkStream.Read(buffer, 0, BufferSize);
                        memoryStream.Write(buffer, 0, bytesReadCount);
                    } while (_networkStream.DataAvailable);
                }

                return memoryStream.GetBuffer();
            }
            catch
            {
                throw;
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }
}
