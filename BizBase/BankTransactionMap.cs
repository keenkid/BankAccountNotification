using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using SunGard.AvantGard.Solution.Ban.Common;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class BankTransactionMap : IBankTransactionMap
    {
        private static List<TransactionBrief> __transList = null;

        public BankTransactionMap() { }

        static BankTransactionMap()
        {
            __transList = new List<TransactionBrief>();
            string asmPath = GetBankAssemblyPath();
            Dictionary<Assembly, string> assemblies = PickupValidateAssembly(asmPath);
            FetchTransactionType(assemblies);
        }

        private static string GetBankAssemblyPath()
        {
            string selfLocation = typeof(BankTransactionMap).Assembly.Location;
            string folder = Directory.GetParent(selfLocation).FullName;
            string path = Path.Combine(folder, SystemFolder.BanksLocation);
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path);
            }
            return path;
        }

        private static Dictionary<Assembly, string> PickupValidateAssembly(string assemblyPath)
        {
            Dictionary<Assembly, string> assemblies = new Dictionary<Assembly, string>();
            var asmFiles = Directory.GetFiles(assemblyPath, "*.dll");
            foreach (var filename in asmFiles)
            {
                Assembly asm = Assembly.LoadFrom(filename);
                var arr = asm.GetCustomAttributes(typeof(BankCodeAttribute), false);
                if (!arr.Any())
                {
                    continue;
                }
                var code = (arr.First() as BankCodeAttribute).BankCode;
                assemblies.Add(asm, code);
            }
            return assemblies;
        }

        private static void FetchTransactionType(Dictionary<Assembly, string> assemblies)
        {
            foreach (var asm in assemblies.Keys)
            {
                Type[] tps = asm.GetExportedTypes();
                foreach (Type tp in tps)
                {
                    var arr = tp.GetCustomAttributes(typeof(TransCodeAttribute), false);
                    if (!arr.Any())
                    {
                        continue;
                    }
                    var transCode = (arr.First() as TransCodeAttribute).TransCode;

                    arr = tp.GetCustomAttributes(typeof(PrefixAttribute), false);
                    var prefix = string.Empty;
                    if (!arr.Any())
                    {
                        prefix = string.Empty;
                    }
                    else
                    {
                        prefix = (arr.First() as PrefixAttribute).Prefix;
                    }

                    __transList.Add(new TransactionBrief()
                    {
                        BankCode = assemblies[asm],
                        TransCode = transCode,
                        TransPrefix = prefix,
                        TransactionType = tp
                    });
                }
            }
        }

        public List<TransactionBrief> BankTransactions
        {
            get { return __transList; }
        }

        public TransactionBrief QueryTransaction(string bankCode,string transCode)
        {
            if (string.IsNullOrEmpty(bankCode) || string.IsNullOrEmpty(transCode))
            {
                throw new ArgumentException("bankCode or transCode");
            }
            foreach (TransactionBrief trans in BankTransactions)
            {
                if (string.Equals(bankCode, trans.BankCode, StringComparison.CurrentCultureIgnoreCase)
                    && string.Equals(transCode, trans.TransCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    return trans;
                }
            }
            return null;
        }
    }
}
