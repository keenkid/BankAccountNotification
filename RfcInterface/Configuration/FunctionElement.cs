using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class FunctionElement : ConfigurationElement
    {
        private const string ELMT_HEADER = "Header";

        private const string ELMT_BODY = "Body";

        private const string ELMT_RETURN = "Return";

        private const string PROP_FUNCTION = "name";

        [ConfigurationProperty(PROP_FUNCTION, IsKey = true)]
        public string FunctionName
        {
            get
            {
                return this[PROP_FUNCTION].ToString();
            }
        }

        [ConfigurationProperty(ELMT_HEADER, IsRequired = true)]
        public HeaderElement Header
        {
            get
            {
                return this[ELMT_HEADER] as HeaderElement;
            }
        }

        [ConfigurationProperty(ELMT_BODY, IsRequired = true)]
        public BodyElement Body
        {
            get
            {
                return this[ELMT_BODY] as BodyElement;
            }
        }

        [ConfigurationProperty(ELMT_RETURN, IsRequired = true)]
        public ReturnElement Feedback
        {
            get
            {
                return this[ELMT_RETURN] as ReturnElement;
            }
        }
    }
}
