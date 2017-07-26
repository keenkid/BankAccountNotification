using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.FileMonitor
{
    public class TimerMonitor : AbstractMonitorFactory
    {
        CustomizeTimer _timer = null;

        private static object obj = new object();

        public override void LaunchMonitor(List<IMonitorObserver> observers)
        {
            _observers = observers;

            foreach (IMonitorObserver observer in _observers)
            {
                _timer = new CustomizeTimer(observer.MonitorData.ReLaunchInterval);
                _timer.Tag = observer.MonitorData.MonitorFolder;
                _timer.AutoReset = true;
                _timer.Enabled = true;
                _timer.Elapsed += OnElapsed;
                _timer.Start();
            }
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            lock (obj)
            {
                var timer = sender as CustomizeTimer;

                List<string> filenames = new List<string>();

                filenames.AddRange(Directory.GetFiles(timer.Tag));

                if (filenames.Any())
                {
                    HandleEachFile(filenames);
                }
            }
        }

        private void HandleEachFile(List<string> filenames)
        {
            foreach (string filename in filenames)
            {
                MonitorHandler(filename);
            }
        }

        public override void DisposeMonitor()
        {
            if (null != _timer)
            {
                _timer.Stop();
                _timer.Close();
            }
        }
    }
}
