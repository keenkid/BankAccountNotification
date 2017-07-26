using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common.Logging;
using SunGard.AvantGard.Solution.Ban.Common;
using System.IO;
using SunGard.AvantGard.Solution.Ban.BizBase;

namespace SunGard.AvantGard.Solution.Ban.Transformation
{
    public abstract class BankResponseBase : IBankResponse
    {
        private static BankSection _section = null;

        static BankResponseBase()
        {
            if (null == _section)
            {
                _section = BankSection.Instance;
            }
        }

        protected BankElement Bank
        {
            get
            {
                if (null == _header || string.IsNullOrEmpty(_header.BankCode))
                {
                    throw new Exception("object not initialized");
                }
                return _section.Banks[_header.BankCode];
            }
        }

        protected TransactionElement Transaction
        {
            get
            {
                if (null == _header || string.IsNullOrEmpty(_header.TransCode))
                {
                    throw new Exception("object not initialized");
                }
                return Bank.TransactionList[_header.TransCode];
            }
        }

        public string BankMessage
        {
            get;
            set;
        }

        protected string FinalMessage
        {
            get;
            set;
        }

        protected virtual bool SaveAsFile
        {
            get
            {
                return true;
            }
        }

        public ILog Logger
        {
            get;
            set;
        }

        public TransactionBrief TransactionBrief
        {
            get;
            set;
        }

        protected SunGardMessageHeader _header = null;

        protected BankResponseBase()
        {
            _header = new SunGardMessageHeader();
        }

        public void Handle()
        {
            try
            {
                ParseMessage();

                StoreMessage();
            }
            catch
            {
                throw;
            }
        }

        private void ParseMessage()
        {
            BuildMessageHeader();

            PreParsePacket();

            ParsePacket();

            PostParsePacket();
        }

        private void BuildMessageHeader()
        {
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyyMMdd");
            string time = dt.ToString("HHmmss");

            _header.TransDate = date;
            _header.TransTime = time;
            _header.BankCode = TransactionBrief.BankCode;
            _header.TransCode = TransactionBrief.TransCode;
            _header.RefID = string.Format("{0}{1}{2:D10}", TransactionBrief.TransPrefix, _header.TransDate, SequenceProvider.Instance.NextSeq());
        }

        protected virtual void PreParsePacket()
        {
        }

        protected virtual void ParsePacket()
        {

        }

        protected virtual void PostParsePacket() { }

        protected virtual void StoreMessage()
        {
            if (SaveAsFile)
            {
                StoreInDisk();
            }
        }

        private void StoreInDisk()
        {
            string filename = string.Format("{0}_{1}.xml", Transaction.Response.Prefix, _header.RefID);
            string fqn = string.Format("{0}\\{1}", SystemFolder.ResponseFolder, filename);
            byte[] buffer = Encoding.UTF8.GetBytes(FinalMessage);
            using (FileStream fs = new FileStream(fqn, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush(true);
            }
        }
    }
}
