using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Threading;

namespace SunGard.AvantGard.Solution.Ban.BanService
{
    public partial class BanService : ServiceBase
    {
        private Thread _thread;

        private BanServiceHost _host = null;

        public BanService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                this.EventLog.WriteEntry("Bank account notification service goint to start.", EventLogEntryType.Information);

                _thread = new Thread(DoWork);

                _thread.Start(this.EventLog);

                this.EventLog.WriteEntry("Bank account notification service start success.");
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }

        private void DoWork(object argsObject)
        {
            if (null == _host)
            {
                EventLog eventLog = argsObject as EventLog;

                _host = new BanServiceHost(eventLog);
            }
            _host.OnStart();
        }

        protected override void OnStop()
        {            
            try
            {
                this.EventLog.WriteEntry("Bank account notification service goint to stop.", EventLogEntryType.Information);
                if (null != _host)
                {
                    _host.OnStop();
                }
                if (null != _thread)
                {
                    _thread.Abort();
                }
                this.EventLog.WriteEntry("Bank account notification service stop success.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
