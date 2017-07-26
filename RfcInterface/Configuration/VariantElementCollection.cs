using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.RfcInterface
{
    public class VariantElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new VariantElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((VariantElement)element).RfcData;
        }

        protected override string ElementName
        {
            get { return "var"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public new VariantElement this[string name]
        {
            get { return (VariantElement)BaseGet(name); }
        }

        public VariantElement this[int index]
        {
            get { return (VariantElement)BaseGet(index); }
        }
    }
}
