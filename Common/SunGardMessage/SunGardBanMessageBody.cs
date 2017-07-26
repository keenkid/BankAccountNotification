using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SunGardBanMessageBody : SunGardMessage
    {
        [XmlTag(Tag = "refID")]
        public string RefID { get; set; }

        [XmlTag(Tag = "bankRefID")]
        public string BankRefID { get; set; }

        [XmlTag(Tag = "transDate")]
        public string TransDate { get; set; }

        [XmlTag(Tag = "transTime")]
        public string TransTime { get; set; }

        [XmlTag(Tag = "acctNo")]
        public string AcctNo { get; set; }

        [XmlTag(Tag = "acctName")]
        public string AcctName { get; set; }

        [XmlTag(Tag = "amount")]
        public string Amount { get; set; }

        [XmlTag(Tag = "currType")]
        public string Currency { get; set; }

        [XmlTag(Tag = "balance")]
        public string Balance { get; set; }

        [XmlTag(Tag = "cptyAcctNo")]
        public string CptyAcctNo { get; set; }

        [XmlTag(Tag = "cptyAcctName")]
        public string CptyAcctName { get; set; }

        [XmlTag(Tag = "cptyBankName")]
        public string CptyBankName { get; set; }

        [XmlTag(Tag = "remark")]
        public string Remark { get; set; }

        [XmlTag(Tag = "abstract")]
        public string Abstract { get; set; }

        public override string ToString()
        {
            return string.Format("<list>{0}</list>", base.ToString());
        }
    }
}
