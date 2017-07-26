using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common;
using SunGard.AvantGard.Solution.Ban.Transformation;
using System.Xml.Linq;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.BCOM
{
    [Prefix("BN")]
    [TransCode("1022")]
    public class BankAccountNotification : AbstractBanResponse
    {
        private const string ROOT_TAG = "notice_text";

        private const string FIELDS_FILE_NAME = "BanFields.txt";

        private const char SPLITOR = '/';

        private static readonly string __FileLocation = string.Empty;

        private static readonly int __FieldsCount = 0;

        private static readonly string __Tags = null;

        static BankAccountNotification()
        {
            var location = typeof(BankAccountNotification).Assembly.Location;
            var parentLocation = Directory.GetParent(location).FullName;
            __FileLocation = Path.Combine(parentLocation, FIELDS_FILE_NAME);

            try
            {
                string text = string.Empty;
                using (StreamReader sr = new StreamReader(__FileLocation, Encoding.UTF8))
                {
                    text = sr.ReadToEnd();                    
                }
                var tags = text.Split(SPLITOR);
                __FieldsCount = tags.Length;

                StringBuilder builder = new StringBuilder();

                int idx = 0;
                string tag = string.Empty;
                foreach (var str in tags)
                {
                    tag = str.Trim();
                    if (string.IsNullOrEmpty(tag))
                    {
                        tag = "Empty";
                    }
                    builder.AppendFormat("<{0}>{1}</{0}>", tag, "{" + idx++ + "}");
                }
                __Tags = builder.ToString();
            }
            catch (Exception)
            {

            }
        }

        protected override bool SaveAsFile
        {
            get
            {
                return true;
            }
        }

        protected override void PreParsePacket()
        {
            BankMessage = BankMessage.Substring(7);

            UpdateBankMessage();
        }

        private void UpdateBankMessage()
        {
            XDocument xdoc = XDocument.Parse(BankMessage);

            try
            {
                var text = xdoc.Descendants(ROOT_TAG).FirstOrDefault().Value;
                var valItems = text.Split(SPLITOR);                
                if (valItems.Length != __FieldsCount)
                {
                    throw new Exception("bank notification message format is different from template, may be BCOM update the format, please contact bank.");
                }
                var content = string.Format(__Tags, valItems);
                BankMessage = string.Format("<{0}>{1}</{0}>", ROOT_TAG, content);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(ex.Message);
                throw;
            }
        }
    }
}
