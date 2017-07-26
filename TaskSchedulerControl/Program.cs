using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.TaskSchedulerControl
{
    class Program
    {
        /// <summary>
        /// args[0] 0:disable 1:enable
        /// args[1] bankCode
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                return;
            }
            string status = args[0];
            string bankCode = args[1];
            using (ITaskControlManager tcm = new TaskControlManager(bankCode))
            {
                if (string.Equals(status, "0"))
                {
                    tcm.DisableTask();
                }
                else if (string.Equals(status, "1"))
                {
                    tcm.EnableTask();
                }
            }
        }
    }
}
