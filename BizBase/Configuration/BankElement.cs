using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class BankElement : ConfigurationElement
    {
        private const string PROP_CODE = "bankCode";

        private const string PROP_NAME = "bankName";

        private const string PROP_CLIENT = "client";

        private const string ELMT_TRANS = "TransList";

        [ConfigurationProperty(PROP_CODE, IsRequired = true, IsKey = true)]
        public string BankCode
        {
            get
            {
                return this[PROP_CODE].ToString();
            }
        }

        [ConfigurationProperty(PROP_NAME, IsRequired = true)]
        public string BankName
        {
            get
            {
                return this[PROP_NAME].ToString();
            }
        }

        [ConfigurationProperty(PROP_CLIENT, IsRequired = true)]
        public string ClientAddress
        {
            get
            {
                return this[PROP_CLIENT].ToString();
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public TransactionElementCollection TransactionList
        {
            get
            {
                return this[""] as TransactionElementCollection;
            }
        }
    }
}
