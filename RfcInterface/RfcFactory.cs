using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;
using SunGard.AvantGard.Solution.Ban.Common;
using System.Xml.Linq;
using SunGard.AvantGard.Solution.Ban.FileMonitor;
using System.IO;
using System.Threading;
using SunGard.AvantGard.Solution.Ban.Common.Logging;
using System.Xml;
using System.Xml.Schema;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public delegate string AsyncRFCSend(XDocument xdoc);

    public class RfcFactory : IMonitorObserver
    {
        private static RfcFunctionSection __section = null;
        private static ILog __logger = null;

        private const string TAG_HEAD = "head";
        private const string TAG_LIST = "list";
        private const string DATE_FORMAT = "yyyyMMdd";

        private static string __messageIdentifier = string.Empty;
        private static string __startupDate = DateTime.Now.ToString(DATE_FORMAT);

        static RfcFactory()
        {
            __section = RfcFunctionSection.Instance;
            __logger = new Log4NetLogFactory().GetLog("rfc");

            OnlineAccounts = OnlineAccountsProvider.Instance.GetOnlineAccounts();
            OnlineBanks = OnlineBanksProvider.Instance.GetOnlineBankCode();
        }

        public static List<string> OnlineAccounts { get; set; }

        public static string OnlineBanks { get; set; }

        public MonitorContent MonitorData
        {
            get
            {
                return new MonitorContent()
                {
                    Name = "RFC Interface",
                    MonitorFolder = SystemFolder.ResponseFolder,
                    ReLaunchInterval = 60 * 1000
                };
            }
        }

        public ActionResult MonitorAction(string filename)
        {
            ActionResult ar = new ActionResult()
            {
                ActionSender = MonitorData.Name,
                SourceFileName = filename,
                Code = ResultCode.S
            };

            RefreshOnlineAccounts();

            while (true)
            {
                try
                {
                    if (File.Exists(filename))
                    {
                        __logger.InfoFormat("Start to send file [{0}]", filename.Remove(0, filename.LastIndexOf("\\") + 1));

                        using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
                        {
                            string message = reader.ReadToEnd().Trim();
                            XDocument xdoc = XDocument.Parse(message);

                            try
                            {
                                Exception exp = null;
                                if (!AssertDataValid(xdoc, out exp) && null != exp)
                                {
                                    __logger.ErrorFormat("File format is invalid.");
                                    throw exp;
                                }
                                else
                                {
                                    AssignMessageIdentifier(xdoc);
                                    AsyncSend(xdoc);
                                    //Send(xdoc);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex is RfcBaseException)
                                {
                                    ar.Code = ResultCode.R;
                                }
                                else
                                {
                                    ar.Code = ResultCode.F;
                                }
                                ar.AppException = ex;
                            }
                        }
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is XmlException)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            }

            return ar;
        }

        private void RefreshOnlineAccounts()
        {
            try
            {
                string today = DateTime.Now.ToString(DATE_FORMAT);
                if (today != __startupDate)
                {
                    OnlineAccounts = OnlineAccountsProvider.Instance.GetOnlineAccounts();
                    OnlineBanks = OnlineBanksProvider.Instance.GetOnlineBankCode();
                    __startupDate = today;
                    __logger.InfoFormat("Refresh online accounts success.");
                }
            }
            catch (Exception ex)
            {
                __logger.ErrorFormat("Try refresh online accounts failed.{0}", ex.Message);
            }
        }

        private bool AssertDataValid(XDocument xdoc, out Exception ex)
        {
            try
            {
                ex = null;
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(Res.BanCheck)))
                {
                    XmlReader reader = XmlReader.Create(stream);
                    var schemas = new XmlSchemaSet();
                    schemas.Add(null, reader);
                    bool isValid = true;
                    Exception exception = null;

                    xdoc.Validate(schemas, (sender, e) =>
                    {
                        isValid = false;
                        exception = e.Exception;
                    });
                    ex = exception;
                    return isValid;
                }
            }
            catch
            {
                throw;
            }
        }

        private void AsyncSend(XDocument xdoc)
        {
            AsyncRFCSend rfcSend = new AsyncRFCSend(Send);
            IAsyncResult ar = rfcSend.BeginInvoke(xdoc, SendCallback, null);
            bool assertCompleted = ar.AsyncWaitHandle.WaitOne(__section.SystemEnvironment.RfcConnectionTimeout * 1000);
            if (!assertCompleted)
            {
                __logger.ErrorFormat("RFC Connection timeout under {0} seconds.", __section.SystemEnvironment.RfcConnectionTimeout);
                throw new RfcConnectionTimeoutException();
            }
        }

        private void SendCallback(IAsyncResult result) { }

        private static string Send(XDocument xdoc)
        {
            try
            {
                RfcDestination destination = ChooseDestination(xdoc);
                RfcRepository repository = destination.Repository;
                IRfcFunction function = repository.CreateFunction(__section.ArFunction.FunctionName);

                BuildHeader(function, xdoc);

                BuildBody(function, repository, xdoc);

                return FunctionInvokeAndReturn(function, destination);
                //return string.Empty;
            }
            catch (Exception ex)
            {
                __logger.ErrorFormat("Send {0} failure:{1}\r\n", __messageIdentifier, ex.Message);
                throw ex;
            }
        }

        private static RfcDestination ChooseDestination(XDocument xdoc)
        {
            if (null == xdoc)
            {
                throw new ArgumentNullException("xdoc");
            }

            string accountNo = xdoc.Descendants("acctNo").FirstOrDefault().Value;
            string bankCode = xdoc.Descendants("bankCode").FirstOrDefault().Value;

            if (null == OnlineAccounts)
            {
                throw new Exception("Online accounts is not initialized.");
            }
            if (OnlineAccounts.Contains(accountNo) && OnlineBanks.Contains(bankCode))
            {
                __logger.InfoFormat("Notification will be send to [PROD] environment");
                return RfcDestinationManager.GetDestination("PROD");
            }
            else
            {
                __logger.InfoFormat("Notification will be send to [TEST] environment");
                return RfcDestinationManager.GetDestination("TEST");
            }
        }

        private static void BuildHeader(IRfcFunction function, XDocument xdoc)
        {
            HeaderElement header = __section.ArFunction.Header;

            IRfcStructure headStruct = function.GetStructure((header.StructName));

            var headPart = xdoc.Descendants(TAG_HEAD).FirstOrDefault();
            string data = string.Empty;

            foreach (VariantElement var in header.Items)
            {
                data = headPart.Descendants(var.Tag).FirstOrDefault().Value;
                data = null == data ? string.Empty : data;
                headStruct.SetValue(var.RfcData, data);
            }
        }

        private static void BuildBody(IRfcFunction function, RfcRepository repository, XDocument xdoc)
        {
            BodyElement body = __section.ArFunction.Body;
            IRfcTable table = function.GetTable(body.TableName);
            IRfcStructure rowStruct = null;

            var list = xdoc.Descendants(TAG_LIST);
            string data = string.Empty;

            foreach (var elmt in list)
            {
                rowStruct = repository.GetStructureMetadata(body.StructName).CreateStructure();
                foreach (VariantElement var in body.Items)
                {
                    data = elmt.Descendants(var.Tag).FirstOrDefault().Value;
                    data = null == data ? string.Empty : data;
                    rowStruct.SetValue(var.RfcData, data);
                    __logger.InfoFormat("Body struct tag=[{0}],value=[{1}]", var.RfcData, data);
                }
                table.Insert(rowStruct);
            }
        }

        private static string FunctionInvokeAndReturn(IRfcFunction function, RfcDestination destination)
        {
            ReturnElement @return = __section.ArFunction.Feedback;

            function.Invoke(destination);

            string rtnCode = function.GetString(@return.ReturnCode);
            string rtnMsg = function.GetString(@return.ReturnMessage);

            if (rtnCode == "S")
            {
                __logger.InfoFormat("{0} send success.\r\n", __messageIdentifier);
            }
            return rtnCode;
        }

        private static void AssignMessageIdentifier(XDocument xdoc)
        {
            var transCode = xdoc.Descendants("transCode").FirstOrDefault().Value;

            switch (transCode)
            {
                case "1002":
                    __messageIdentifier = "Today Statements";
                    break;
                case "1003":
                    __messageIdentifier = "History Statements";
                    break;
                case "1022":
                    __messageIdentifier = "Bank Account Notification";
                    break;
                default:
                    __messageIdentifier = "Unknow name";
                    break;
            }
        }
    }
}
