using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SequenceProvider
    {
        protected SequenceProvider() 
        {
            Initialize();
        }

        private int _seqNo = 0;
        private string _todayString = string.Empty;

        private const string SPLITOR = "|";

        private string _filepath = string.Empty;

        private static object syncRoot = new object();

        private static volatile SequenceProvider __instance = null;

        public static SequenceProvider Instance
        {
            get
            {
                if (null == __instance)
                {
                    lock (syncRoot)
                    {
                        if (null == __instance)
                        {
                            __instance = new SequenceProvider();
                        }
                    }
                }
                return __instance;
            }
        }

        private void Initialize()
        {
            string filename = "seqno";
            _filepath = Path.Combine(SystemFolder.RuntimeFolder, filename);
            if (!File.Exists(_filepath))
            {
                FileStream stream = File.Create(_filepath);
                stream.Close();
            }
            ReadSequenceFile();
        }

        private void ReadSequenceFile()
        {
            string item = string.Empty;
            string ts = DateTime.Now.ToString("yyyyMMdd");

            FileStream fs = File.Open(_filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                item = sr.ReadToEnd().Trim();
            }
            if (string.IsNullOrEmpty(item))
            {
                _todayString = ts;
                _seqNo = 1;
            }
            else
            {
                string[] arr = item.Split(new string[] { SPLITOR }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 2)
                {
                    _todayString = arr[0];
                    if (_todayString != ts)
                    {
                        _todayString = ts;
                        _seqNo = 1;
                    }
                    else
                    {
                        if (!int.TryParse(arr[1], out _seqNo))
                        {
                            _seqNo = 1;
                        }
                    }
                }
                else
                {
                    _todayString = ts;
                    _seqNo = 1;
                }
            }
        }

        public int NextSeq()
        {
            string ts = DateTime.Now.ToString("yyyyMMdd");
            if (_todayString != ts)
            {
                ReadSequenceFile();
            }
            return _seqNo++;
        }

        public void FlushSeq()
        {
            if (File.Exists(_filepath))
            {
                using (StreamWriter sw = new StreamWriter(_filepath, false, Encoding.UTF8))
                {
                    sw.Write(string.Format("{0}{1}{2}", _todayString, SPLITOR, _seqNo));
                    sw.Flush();
                }
            }
        }
    }
}
