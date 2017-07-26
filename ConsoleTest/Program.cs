using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using SunGard.AvantGard.Solution.Ban.NetworkListener;
using SunGard.AvantGard.Solution.Ban.BizBase;
using SunGard.AvantGard.Solution.Ban.RfcInterface;
using SunGard.AvantGard.Solution.Ban.BanService;
using SunGard.AvantGard.Solution.Ban.Formatting;
using System.Text.RegularExpressions;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //DbOperation db = new DbOperation();

            //string companyCodes = "'CONTI'";

            //List<string> accounts = db.GetOnlineAccounts(companyCodes);

            //RfcFactory.OnlineAccounts = accounts;

            try
            {
                RfcFactory factory = new RfcFactory();
                factory.MonitorAction(@"C:\D\Projects\Wilmar\AccountNotification\master\build\BankAccountNotification\data\response\BankAccountNotification_201505130000000000.xml");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            //BanServiceHost host = new BanServiceHost(null);
            //host.OnStart();

            //Console.WriteLine("Print [QUIT] to exit:");
            //string input = Console.ReadLine();

            //while (input.ToLower() != "quit")
            //{
            //    Console.WriteLine("Print [QUIT] to exit:");
            //    input = Console.ReadLine();
            //}
            //host.OnStop();
        }
    }
}
