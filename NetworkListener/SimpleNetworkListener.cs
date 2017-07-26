using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using SunGard.AvantGard.Solution.Ban.NetworkListener.Configuration;
using SunGard.AvantGard.Solution.Ban.Common.Logging;
using System.Threading;
using System.Net;
using SunGard.AvantGard.Solution.Ban.Common;
using SunGard.AvantGard.Solution.Ban.BizBase;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace SunGard.AvantGard.Solution.Ban.NetworkListener
{
    public class SimpleNetworkListener : AbstractNetworkListener
    {
        private TcpListener _listener = null;

        private static ManualResetEvent clientConnect = new ManualResetEvent(false);

        public SimpleNetworkListener()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (null != _listener)
            {
                return;
            }

            _listener = new TcpListener(_section.Terminate.ListenIP, _section.Terminate.ListenPort);
        }

        public override void Start()
        {
            if (null == _listener)
            {
                _logger.Error("Tcp listener initialize failure, please check configuration file");
            }
            try
            {
                _listener.Start();
                _logger.InfoFormat("TCP listener start success, IP: [{0}] Port: [{1}]", _section.Terminate.ListenIP, _section.Terminate.ListenPort);

                StartService();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("TCP listener start failure:\r\n{0}", ex.Message);
            }
        }

        public override void Stop()
        {
            try
            {
                _listener.Stop();
                _logger.Info("Tcp listener stop success.");
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Tcp listener stop failure:\r\n{0}", ex.Message);
            }
        }

        private void StartService()
        {
            try
            {
                do
                {
                    clientConnect.Reset();
                    _listener.BeginAcceptTcpClient(AcceptTcpClientCallBack, _listener);
                } while (clientConnect.WaitOne());
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Error:\r\n{0}", ex.Message);
                if (ex is SocketException)
                {
                    if ((ex as SocketException).SocketErrorCode == SocketError.ConnectionReset)
                    {
                        _logger.Info("Connection was reset by remote client,");
                    }
                }
            }
        }

        private void AcceptTcpClientCallBack(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = ar.AsyncState as TcpListener;
                TcpClient client = listener.EndAcceptTcpClient(ar);

                if (ThreadPool.QueueUserWorkItem(TcpClientConnect, client))
                {
                    clientConnect.Set();
                }
            }
            catch
            {
                throw;
            }
        }

        private void TcpClientConnect(object tcpClient)
        {
            TcpClient client = tcpClient as TcpClient;

            EndPoint ep = null;
            try
            {
                ep = client.Client.RemoteEndPoint;
                _logger.InfoFormat("Client [{0}] connected.", ep);

                IBanData banData = new BanData();
                if (!banData.ClientValidation(ep))
                {
                    throw new Exception(string.Format("Client [{0}] is not authorized.", ep));
                }

                ISocketMessage sm = new TcpClientMessage(client, _section.Environment);
                string message = ReceiveMessage(sm);
                bool isValid = AssertReceivedMessage(message);
                bool storeSuccess = false;
                if (isValid)
                {
                    storeSuccess = Store(message, banData.QueryClientAddenda(ep));
                }
                ResponseMessage(sm, isValid && storeSuccess);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                client.Close();
                _logger.InfoFormat("Client [{0}] closed.", ep);
            }
        }

        private bool AssertReceivedMessage(string content)
        {
            try
            {
                XDocument.Parse(content);
                return true;
            }
            catch (XmlException ex)
            {
                _logger.ErrorFormat("Received message not completed.{0}", ex.Message);
                return false;
            }
        }

        private bool Store(string message, string addenda)
        {
            try
            {
                System.Threading.Thread.Sleep(1);
                string filename = string.Format("{0}{1:yyyyMMddHHmmssffffff}.txt", addenda, DateTime.Now);
                string fqn = Path.Combine(SystemFolder.ResponseTempFolder, filename);
                using (FileStream fs = new FileStream(fqn, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    string content = string.Format("{0}{1}", addenda, message);
                    byte[] buffer = Encoding.UTF8.GetBytes(content);
                    fs.Write(buffer, 0, buffer.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Store message error.{0}", ex.Message);
                return false;
            }
        }
    }
}
