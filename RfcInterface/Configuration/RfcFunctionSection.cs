using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class RfcFunctionSection : ConfigurationSection
    {
        private const string SECTION_NAME = "RfcFunctionSection";

        private const string ELMT_RFCFUNCTION = "Function";

        private const string ELMT_SYSTEM = "System";

        private static object obj = new object();

        private static RfcFunctionSection __instance = null;

        protected RfcFunctionSection() { }

        [ConfigurationProperty(ELMT_RFCFUNCTION, IsRequired = true)]
        public FunctionElement ArFunction
        {
            get
            {
                return this[ELMT_RFCFUNCTION] as FunctionElement;
            }
        }

        [ConfigurationProperty(ELMT_SYSTEM, IsRequired = true)]
        public SystemElement SystemEnvironment
        {
            get
            {
                return this[ELMT_SYSTEM] as SystemElement;
            }
        }

        public static RfcFunctionSection Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (obj)
                    {
                        var asmLocation = typeof(RfcFunctionSection).Assembly.Location;
                        var config = ConfigurationManager.OpenExeConfiguration(asmLocation);
                        var section = config.GetSection(SECTION_NAME) as RfcFunctionSection;
                        __instance = section;
                    }
                }
                return __instance;
            }
        }
    }
}
