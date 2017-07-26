using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class BankCodeAttribute : Attribute
    {
        private string _bankCode = string.Empty;

        public BankCodeAttribute(string bankCode)
        {
            _bankCode = bankCode;
        }

        public string BankCode
        {
            get
            {
                return _bankCode;
            }
            private set
            {
                _bankCode = value;
            }
        }
    }
}
