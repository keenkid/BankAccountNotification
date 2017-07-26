using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.ABC
{
    class AbcFieldSection : ConfigurationSection
    {
        private const string SECTION_NAME = "AbcFieldSection";

        private static object obj = new object();

        private static AbcFieldSection __instance = null;

        protected AbcFieldSection() { }

        public static AbcFieldSection Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (obj)
                    {
                        var asmLocation = typeof(AbcFieldSection).Assembly.Location;
                        var config = ConfigurationManager.OpenExeConfiguration(asmLocation);
                        var section = config.GetSection(SECTION_NAME) as AbcFieldSection;
                        __instance = section;
                    }
                }
                return __instance;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
        public FieldElementCollection Fields
        {
            get
            {
                return this[""] as FieldElementCollection;
            }
        }
    }
}
