using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    [Serializable]
    public class SunGardMessageHeader : SunGardMessage
    {
        [XmlTag(Tag = "bankCode")]
        public string BankCode { get; set; }

        [XmlTag(Tag = "transCode")]
        public string TransCode { get; set; }

        [XmlTag(Tag = "transDate")]
        public string TransDate { get; set; }

        [XmlTag(Tag = "transTime")]
        public string TransTime { get; set; }

        [XmlTag(Tag = "refID")]
        public string RefID { get; set; }
    }
}
