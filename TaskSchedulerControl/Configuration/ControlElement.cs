using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.TaskSchedulerControl
{
    class ControlElement : ConfigurationElement
    {
        private const string ATTR_BANKCODE = "bankCode";

        private const string ATTR_TASKNAME = "taskName";

        [ConfigurationProperty(ATTR_BANKCODE, IsKey = true, IsRequired = false)]
        public string BankCode
        {
            get
            {
                return this[ATTR_BANKCODE] as string;
            }
        }

        [ConfigurationProperty(ATTR_TASKNAME, IsKey = false, IsRequired = false)]
        public string TaskName
        {
            get
            {
                return this[ATTR_TASKNAME] as string;
            }
        }
    }
}
