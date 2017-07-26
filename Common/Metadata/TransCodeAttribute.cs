using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class TransCodeAttribute : Attribute
    {
        private string _transCode = string.Empty;

        public TransCodeAttribute(string transCode)
        {
            _transCode = transCode;
        }

        public string TransCode
        {
            get
            {
                return _transCode;
            }
            private set
            {
                _transCode = value;
            }
        }
    }
}
