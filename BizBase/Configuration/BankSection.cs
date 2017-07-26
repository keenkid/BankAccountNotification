using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class BankSection : ConfigurationSection
    {
        private const string SECTION_NAME = "BankSection";

        private static object obj = new object();

        private static BankSection __instance = null;

        protected BankSection() { }

        public static BankSection Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (obj)
                    {
                        var asmLocation = typeof(BankSection).Assembly.Location;
                        var config = ConfigurationManager.OpenExeConfiguration(asmLocation);
                        var section = config.GetSection(SECTION_NAME) as BankSection;
                        __instance = section;
                    }
                }
                return __instance;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public BankElementCollection Banks
        {
            get
            {
                return this[""] as BankElementCollection;
            }
        }
    }
}
