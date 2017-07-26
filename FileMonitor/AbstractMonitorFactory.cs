using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common.Logging;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using SunGard.AvantGard.Solution.Ban.Common;

namespace SunGard.AvantGard.Solution.Ban.FileMonitor
{
    public abstract class AbstractMonitorFactory
    {
        private static object obj = new object();

        protected static ILog __logger = null;

        protected List<IMonitorObserver> _observers = null;

        public abstract void LaunchMonitor(List<IMonitorObserver> observers);

        public abstract void DisposeMonitor();

        static AbstractMonitorFactory()
        {
            __logger = new Log4NetLogFactory().GetLog("transaction");
        }

        protected void MonitorHandler(string filename)
        {
            lock (obj)
            {
                if (!File.Exists(filename))
                {
                    return;
                }

                foreach (IMonitorObserver observer in _observers)
                {
                    if (filename.Substring(0, filename.LastIndexOf(Path.DirectorySeparatorChar)) == observer.MonitorData.MonitorFolder)
                    {
                        var rslt = observer.MonitorAction(filename);

                        HandleActionResult(rslt);
                    }
                }
            }
        }

        private void HandleActionResult(ActionResult ar)
        {
            if (ResultCode.S == ar.Code)
            {
                MoveFile(ar, string.IsNullOrEmpty(ar.Backup2Folder) ? SystemFolder.BackupFolder : SystemFolder.SourceFolder);
            }
            else if (ResultCode.F == ar.Code)
            {
                MoveFile(ar, SystemFolder.ErrorFolder);
            }
            else
            {
                __logger.InfoFormat("{0} will retry file [{1}] again.", ar.ActionSender, ar.SourceFileName.Substring(ar.SourceFileName.LastIndexOf("\\") + 1));
            }
        }

        private void MoveFile(string filename, string newPath)
        {
            try
            {
                if (!Path.IsPathRooted(newPath))
                {
                    newPath = Path.Combine(Directory.GetParent(typeof(AbstractMonitorFactory).Assembly.Location).FullName, newPath);
                }
                string today = DateTime.Now.ToString("yyyyMMdd");
                newPath = Path.Combine(newPath, today);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                newPath = Path.Combine(newPath, filename.Substring(filename.LastIndexOf(@"\") + 1));
                File.Move(filename, newPath);
                __logger.InfoFormat("File [{0}] was moved to [{1}]\r\n", filename, newPath);
            }
            catch (Exception ex)
            {
                __logger.ErrorFormat("Move file encounter error:{0}", ex.Message);
            }
        }

        private void MoveFile(ActionResult ar, string newPath)
        {
            if (null != ar.AppException)
            {
                __logger.ErrorFormat("{1}:{0}", ar.AppException.Message, ar.AppException.GetType().Name);
            }
            MoveFile(ar.SourceFileName, newPath);
        }
    }
}
