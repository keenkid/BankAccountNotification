using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.TaskSchedulerControl
{
    class TaskControlSection : ConfigurationSection
    {
        private const string SECTION_NAME = "taskControlSection";

        private static object obj = new object();

        private static TaskControlSection __instance = null;

        protected TaskControlSection()
        {

        }

        [ConfigurationProperty("",IsDefaultCollection=true)]
        public ControlElementCollection TaskControls
        {
            get
            {
                return this[""] as ControlElementCollection;
            }
        }

        public static TaskControlSection Instance
        {
            get
            {
                lock (obj)
                {
                    if (null == __instance)
                    {
                        var asmLocation = typeof(TaskControlSection).Assembly.Location;
                        var config = ConfigurationManager.OpenExeConfiguration(asmLocation);
                        __instance = config.GetSection(SECTION_NAME) as TaskControlSection;
                    }
                    return __instance;
                }
            }
        }
    }
}
