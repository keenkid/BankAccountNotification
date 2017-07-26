using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.RfcInterface;
using SunGard.AvantGard.Solution.Ban.NetworkListener;
using SunGard.AvantGard.Solution.Ban.FileMonitor;
using System.Reflection;
using System.IO;
using SunGard.AvantGard.Solution.Ban.Common;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SunGard.AvantGard.Solution.Ban.BanService
{
    public class BanServiceHost : MarshalByRefObject
    {
        List<AbstractMonitorFactory> _monitorList = null;
        List<IMonitorObserver> _observerList = null;
        INetworkListener _listener = null;
        ServiceHost _netPipeHost = null;
        EventLog _eventLog = null;

        public BanServiceHost(EventLog eventLog)
        {
            _eventLog = eventLog;

            InitializeMonitor();

            InitializeMonitorObserver();

            InitializeWebService();

            InitializeListener();
        }

        private void InitializeMonitor()
        {
            _monitorList = new List<AbstractMonitorFactory>();
            _monitorList.AddRange(from tp in typeof(AbstractMonitorFactory).Assembly.GetTypes()
                                  where typeof(AbstractMonitorFactory).IsAssignableFrom(tp) && !tp.IsAbstract
                                  select Activator.CreateInstance(tp) as AbstractMonitorFactory);
        }

        private void InitializeMonitorObserver()
        {
            _observerList = new List<IMonitorObserver>();

            string codeBasePath = AppDomain.CurrentDomain.BaseDirectory;

            string[] asmFiles = Directory.GetFiles(codeBasePath, "*.dll");
            foreach (string asmfile in asmFiles)
            {
                try
                {
                    var tps = Assembly.LoadFrom(asmfile).GetExportedTypes();

                    foreach (Type t in tps)
                    {
                        if (!t.IsAbstract && typeof(IMonitorObserver).IsAssignableFrom(t))
                        {
                            _observerList.Add(Activator.CreateInstance(t) as IMonitorObserver);
                            WriteSystemEntry(t.FullName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteSystemEntry(ex.Message);
                }
            }
        }

        private void WriteSystemEntry(string message)
        {
            if (null != _eventLog)
            {
                _eventLog.WriteEntry(message);
            }
        }

        private void InitializeListener()
        {
            _listener = new SimpleNetworkListener();
        }

        private void InitializeWebService()
        {
            Uri baseAddress = new Uri("http://localhost:8000/onlinecompany");
            string address = "net.pipe://localhost/onlinecompany";

            _netPipeHost = new ServiceHost(typeof(OnlineCompanyService), baseAddress);

            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            _netPipeHost.AddServiceEndpoint(typeof(IOnlineCompany), binding, address);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.HttpGetUrl = new Uri("http://localhost:8001/");
            _netPipeHost.Description.Behaviors.Add(smb);
        }

        public void OnStart()
        {
            if (null != _monitorList && 0 < _monitorList.Count)
            {
                _monitorList.ForEach(monitor => monitor.LaunchMonitor(_observerList));
            }

            _netPipeHost.Open();

            _listener.Start();
        }

        public void OnStop()
        {
            SequenceProvider.Instance.FlushSeq();
            //OnlineAccountsProvider.Instance.FlushOnlineAccounts(RfcFactory.OnlineAccounts);

            if (null != _monitorList && 0 < _monitorList.Count)
            {
                _monitorList.ForEach(monitor => monitor.DisposeMonitor());
            }

            _netPipeHost.Close();

            _listener.Stop();
        }
    }
}
