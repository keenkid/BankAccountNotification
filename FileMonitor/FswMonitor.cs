using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.FileMonitor
{
    public class FswMonitor : AbstractMonitorFactory
    {
        FileSystemWatcher watcher = null;

        public override void LaunchMonitor(List<IMonitorObserver> observers)
        {
            _observers = observers;

            foreach (IMonitorObserver observer in _observers)
            {
                watcher = new FileSystemWatcher();

                try
                {
                    watcher.IncludeSubdirectories = false;
                    watcher.Path = observer.MonitorData.MonitorFolder;
                    watcher.InternalBufferSize = 64 * 1024;
                    watcher.NotifyFilter = NotifyFilters.FileName;

                    watcher.Created += FswHandler;

                    watcher.EnableRaisingEvents = true;
                }
                catch (Exception ex)
                {
                    __logger.Error(ex.Message);
                }
            }
        }

        private void FswHandler(object sender, FileSystemEventArgs e)
        {
            string filename = e.FullPath;
            if (filename.ToLower().EndsWith(".xml"))
            {
                MonitorHandler(filename);
            }
        }

        public override void DisposeMonitor()
        {
            if (null != watcher)
            {
                watcher.Dispose();
            }
        }
    }
}
