using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class BankElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BankElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BankElement)element).BankCode;
        }

        protected override string ElementName
        {
            get { return "Bank"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public new BankElement this[string code]
        {
            get { return (BankElement)BaseGet(code); }
        }

        public BankElement this[int index]
        {
            get { return (BankElement)BaseGet(index); }
        }
    }
}
