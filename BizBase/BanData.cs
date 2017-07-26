using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class BanData : IBanData
    {
        private static Dictionary<string, string> _addenda;

        static BanData()
        {
            var section = BankSection.Instance;
            if (null != section)
            {
                _addenda = new Dictionary<string, string>();
                var collection = section.Banks;
                foreach (BankElement be in collection)
                {
                    _addenda.Add(be.ClientAddress, string.Format("{0}1022", be.BankCode));
                }
            }
        }

        protected Dictionary<string, string> ClientAddenda
        {
            get
            {
                return _addenda;
            }
            private set
            {
                _addenda = value;
            }
        }

        public bool ClientValidation(string ipAddress)
        {
            if (ClientAddenda.Keys.Contains(ipAddress))
            {
                return true;
            }
            return false;
        }

        public bool ClientValidation(System.Net.EndPoint ep)
        {
            string ipAddr = GetIPAddress(ep);
            return ClientValidation(ipAddr);
        }

        public string QueryClientAddenda(string ipAddress)
        {
            if (ClientValidation(ipAddress))
            {
                return ClientAddenda[ipAddress];
            }
            return string.Empty;
        }

        public string QueryClientAddenda(System.Net.EndPoint ep)
        {
            string ipAddr = GetIPAddress(ep);
            return QueryClientAddenda(ipAddr);
        }

        private string GetIPAddress(System.Net.EndPoint ep)
        {
            string ipport = ep.ToString();
            string ipAddr = ipport.Substring(0, ipport.IndexOf(":"));
            return ipAddr;
        }
    }
}
