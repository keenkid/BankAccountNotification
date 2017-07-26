using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class OnlineAccountsProvider
    {
        private static object syncRoot = new object();

        private static OnlineAccountsProvider __instance = null;

        private string _filepath = string.Empty;

        protected OnlineAccountsProvider()
        {
            Initialize();
        }

        private void Initialize()
        {
            string filename = "accounts";
            _filepath = Path.Combine(SystemFolder.RuntimeFolder, filename);
            if (!File.Exists(_filepath))
            {
                FileStream stream = File.Create(_filepath);
                stream.Close();
            }
        }

        public static OnlineAccountsProvider Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (syncRoot)
                    {
                        if (null == __instance)
                        {
                            __instance = new OnlineAccountsProvider();
                        }
                    }
                }
                return __instance;
            }
        }

        public List<string> GetOnlineAccounts()
        {
            List<string> accounts = new List<string>();

            IDbAccess dbAccess = new SqlServerAccess();
            accounts = dbAccess.GetOnlineAccounts();
            StoreInFile(accounts);

            return accounts;
        }

        private void StoreInFile(List<string> accounts)
        {
            try
            {
                if (null != accounts)
                {
                    using (StreamWriter sw = new StreamWriter(_filepath, false, Encoding.UTF8))
                    {
                        foreach (var item in accounts)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
            }
            catch
            {
                //
            }
        }
    }
}
