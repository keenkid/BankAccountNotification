using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.BankInterface;
using System.IO;
using System.Configuration;
using SunGard.BankInterface.Configuration;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class ConnectionStringProvider
    {
        protected static ConnectionStringProvider __instance = null;

        private static object syncRoot = new object();

        protected ConnectionStringProvider()
        {
            string installPath = GetBankInterfaceInstallPath();

            GetBankInterfaceConfiguration(installPath);
        }

        public static ConnectionStringProvider Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (syncRoot)
                    {
                        if (null == __instance)
                        {
                            __instance = new ConnectionStringProvider();
                        }
                    }
                }
                return __instance;
            }
        }

        public string ConnectionString
        {
            get;
            private set;
        }

        private string GetBankInterfaceInstallPath()
        {
            using (BankInterfaceRegistryKeyWrapper wrapper = new BankInterfaceRegistryKeyWrapper())
            {
                var installPath = wrapper.GetValue("InstallPath", string.Empty);
                return installPath.ToString();
            }
        }

        private void GetBankInterfaceConfiguration(string installPath)
        {
            if (Directory.Exists(installPath))
            {
                string exeFile = @"bin\SunGard.BankInterface.exe";
                exeFile = Path.Combine(installPath, exeFile);

                Configuration configure = ConfigurationManager.OpenExeConfiguration(exeFile);

                var section = configure.GetSection("sungard.bankInterface") as BankInterfaceSection;

                string connectionName = section.Data.ConnectionString.Name;

                ConnectionString = configure.ConnectionStrings.ConnectionStrings[connectionName].GetConnectionString();
            }
        }
    }
}
