using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class OnlineCompanyCodeProvider
    {
        private static object syncRoot = new object();

        private static OnlineCompanyCodeProvider __instance = null;

        private string _filepath = string.Empty;

        protected OnlineCompanyCodeProvider()
        {
            Initialize();
        }

        public static OnlineCompanyCodeProvider Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (syncRoot)
                    {
                        if (null == __instance)
                        {
                            __instance = new OnlineCompanyCodeProvider();
                        }
                    }
                }
                return __instance;
            }
        }

        private void Initialize()
        {
            string filename = "company";
            _filepath = Path.Combine(SystemFolder.RuntimeFolder, filename);
            if (!File.Exists(_filepath))
            {
                FileStream stream = File.Create(_filepath);
                stream.Close();
            }
        }

        public string GetOnlineCompanyCode()
        {
            string companyCodes = string.Empty;

            using (StreamReader reader = new StreamReader(_filepath, Encoding.UTF8))
            {
                companyCodes = reader.ReadToEnd().Trim();
            }

            return companyCodes;
        }

        public void FlushOnlineCompanyCode(string companyCodes)
        {
            using (StreamWriter writer = new StreamWriter(_filepath, false, Encoding.UTF8))
            {
                writer.WriteLine(companyCodes);
                writer.Flush();
            }
        }
    }
}
