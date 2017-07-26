using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.ABC
{
    class FieldElement : ConfigurationElement
    {
        private const string PROP_NAME = "name";

        private const string PROP_START = "start";

        private const string PROP_END = "end";

        [ConfigurationProperty(PROP_NAME, IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return this[PROP_NAME].ToString();
            }
        }

        [ConfigurationProperty(PROP_START, IsRequired = true)]
        public int Start
        {
            get
            {
                int start = 0;

                if (!int.TryParse(this[PROP_START].ToString(), out start))
                {
                    throw new Exception("invalid configuration");
                }
                return start - 1;
            }
        }

        [ConfigurationProperty(PROP_END, IsRequired = true)]
        public int End
        {
            get
            {
                int end = 0;

                if (!int.TryParse(this[PROP_END].ToString(), out end))
                {
                    throw new Exception("invalid configuration");
                }
                return end - 1;
            }
        }
    }
}
