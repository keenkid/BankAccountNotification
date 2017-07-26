using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class TransactionElement : ConfigurationElement
    {
        private const string PROP_CODE = "transCode";

        private const string PROP_NAME = "transName";

        private const string ELMT_RESP = "Response";

        [ConfigurationProperty(PROP_CODE, IsRequired = true, IsKey = true)]
        public string TransCode
        {
            get
            {
                return this[PROP_CODE].ToString();
            }
        }

        [ConfigurationProperty(PROP_NAME, IsRequired = true)]
        public string TransName
        {
            get
            {
                return this[PROP_NAME].ToString();
            }
        }

        [ConfigurationProperty(ELMT_RESP, IsRequired = true)]
        public ResponseElement Response
        {
            get
            {
                return this[ELMT_RESP] as ResponseElement;
            }
        }
    }
}
