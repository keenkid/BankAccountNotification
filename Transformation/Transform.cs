using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.FileMonitor;
using SunGard.AvantGard.Solution.Ban.Common;
using SunGard.AvantGard.Solution.Ban.BizBase;
using SunGard.AvantGard.Solution.Ban.Common.Logging;
using System.IO;
using System.Threading;

namespace SunGard.AvantGard.Solution.Ban.Transformation
{
    public class Transform : IMonitorObserver
    {
        protected static ILog _logger = null;

        static Transform()
        {
            _logger = new Log4NetLogFactory().GetLog("transform");
        }

        public Transform() { }

        public MonitorContent MonitorData
        {
            get
            {
                return new MonitorContent()
                {
                    Name = "Transform module",
                    MonitorFolder = SystemFolder.ResponseTempFolder,
                    ReLaunchInterval = 2 * 1000
                };
            }
        }

        public ActionResult MonitorAction(string filename)
        {
            string message = string.Empty;
            _logger.InfoFormat("Start to transform message in file [{0}]", filename.Remove(0, filename.LastIndexOf("\\") + 1));
            while (true)
            {
                try
                {
                    if (File.Exists(filename))
                    {
                        using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
                        {
                            message = reader.ReadToEnd().Trim();
                        }
                    }
                    break;
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }

            ActionResult ar = new ActionResult()
            {
                ActionSender = MonitorData.Name,
                SourceFileName = filename,
                Backup2Folder = SystemFolder.SourceFolder,
                Code = ResultCode.S
            };

            try
            {
                ProcessMessage(message);
            }
            catch (Exception ex)
            {
                ar.AppException = ex;
                ar.Code = ResultCode.F;
            }
            return ar;
        }

        private void ProcessMessage(string message)
        {
            if (message.Length <= 6)
            {
                throw new Exception(string.Format("Invalid bank message.\r\n{0}", message));
            }

            string bankCode = message.Substring(0, 2);
            string transCode = message.Substring(2, 4);
            _logger.InfoFormat("Message brief: bank code [{0}],trans code [{1}]", bankCode, transCode);

            IBankTransactionMap map = new BankTransactionMap();
            var trans = map.QueryTransaction(bankCode, transCode);
            if (null == trans)
            {
                _logger.ErrorFormat("Invalid content.\r\n{0}", message);
                return;
            }

            IBankResponse response = Activator.CreateInstance(trans.TransactionType) as IBankResponse;
            if (null == response)
            {
                _logger.ErrorFormat("Invalid transaction type.\r\n{0}", trans.TransactionType);
                return;
            }

            response.BankMessage = message.Substring(6);
            response.TransactionBrief = trans;
            response.Logger = _logger;
            try
            {
                response.Handle();
                _logger.Info("Transform success.\r\n");
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}
