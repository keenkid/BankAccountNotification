using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common;
using SunGard.AvantGard.Solution.Ban.Transformation;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using System.Configuration;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.BOC
{
    [Prefix("BN")]
    [TransCode("1022")]
    public class BankAccountNotification : AbstractBanResponse
    {
        const int ERROR_FILE_NOT_FOUND = 2;
        const int ERROR_ACCESS_DENIED = 5;

        private string _parentLocation = string.Empty;

        private const string FILE_NAME = "SunGard.AvantGard.TaskSchedulerControl.exe";

        public BankAccountNotification()
        {
            _parentLocation = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.FullName;
        }

        protected override bool SaveAsFile
        {
            get
            {
                return true;
            }
        }

        protected override void PostParsePacket()
        {
            base.PostParsePacket();

            new Action(AssertInvokeCondition).BeginInvoke(null, null);
        }

        private void AssertInvokeCondition()
        {
            var item = _bodies.Last();
            var transTime = DateTime.ParseExact(string.Format("{0}-{1}", item.TransDate, item.TransTime), "yyyyMMdd-HHmmss", Thread.CurrentThread.CurrentCulture);
            var ts = DateTime.Now - transTime;
            
            if (ts.TotalMinutes > GetExpiredTime())
            {
                StartQueryFunction();
            }
        }

        private int GetExpiredTime()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                return Convert.ToInt32(config.AppSettings.Settings["expiredTime"].Value);
            }
            catch
            {
                return 20;
            }
        }

        private void StartQueryFunction()
        {
            try
            {
                Process taskProcess = new Process();
                taskProcess.StartInfo.WorkingDirectory = _parentLocation;
                taskProcess.StartInfo.FileName = FILE_NAME;
                taskProcess.StartInfo.Arguments = "1 03";
                taskProcess.StartInfo.CreateNoWindow = true;
                taskProcess.Start();
            }
            catch (Win32Exception e)
            {
                if (e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
                {
                    throw new Exception(e.Message + ".Check the path");
                }
                else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
                {
                    throw new Exception(e.Message + ".You do not have permission to execute this program");
                }
            }
        }
    }
}
