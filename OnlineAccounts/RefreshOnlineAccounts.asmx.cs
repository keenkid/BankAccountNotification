using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ServiceModel;
using SunGard.AvantGard.Solution.Ban.Common;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common.Logging;

namespace SunGard.AvantGard.Solution.Ban.OnlineAccounts
{
    /// <summary>
    /// RefreshOnlineAccounts 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://wilmar.com/refreshaccounts")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class RefreshOnlineAccounts : System.Web.Services.WebService
    {
        private static ILog __logger = null;

        public RefreshOnlineAccounts()
        {
            __logger = new Log4NetLogFactory().GetLog("websrv");
        }

        [WebMethod(MessageName = "RefreshAccounts", Description = "Refresh online accounts")]
        public bool RefreshAccounts(List<OnlineAccount> accountList)
        {
            __logger.InfoFormat("Start to refresh online company codes");
            if (null == accountList || 0 == accountList.Count)
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            foreach (var item in accountList)
            {
                builder.Append(string.Format("'{0}',", item.CompanyCode));
            }
            string codes = builder.Remove(builder.Length - 1, 1).ToString();
            __logger.InfoFormat("Online company codes are:{0}", codes);

            bool rslt = ResendCompanyCodes(codes);
            if (rslt)
            {
                __logger.InfoFormat("Online company codes refresh success.\r\n");
            }
            else
            {
                __logger.ErrorFormat("Online company codes refresh error.\r\n");
            }
            return rslt;
        }

        private bool ResendCompanyCodes(string codes)
        {
            string address = "net.pipe://localhost/onlinecompany";

            try
            {
                using (ChannelFactory<IOnlineCompany> channelFactory = new ChannelFactory<IOnlineCompany>(new NetNamedPipeBinding(NetNamedPipeSecurityMode.None), address))
                {
                    IOnlineCompany proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        string message = proxy.RefreshOnlineCompanyList(codes);
                        if (string.IsNullOrEmpty(message))
                        {
                            return true;
                        }
                        else
                        {
                            __logger.ErrorFormat(message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                __logger.ErrorFormat("Send company code error:{0}", ex.Message);
                return false;
            }
        }
    }
}
