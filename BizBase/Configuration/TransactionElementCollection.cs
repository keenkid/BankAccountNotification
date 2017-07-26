using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.BizBase
{
    public class TransactionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TransactionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TransactionElement)element).TransCode;
        }

        protected override string ElementName
        {
            get { return "Trans"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public new TransactionElement this[string code]
        {
            get { return (TransactionElement)BaseGet(code); }
        }

        public TransactionElement this[int index]
        {
            get { return (TransactionElement)BaseGet(index); }
        }
    }
}
