using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.Solution.Ban.ABC
{
    class FieldElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FieldElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FieldElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "field"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public new FieldElement this[string name]
        {
            get { return (FieldElement)BaseGet(name); }
        }

        public FieldElement this[int index]
        {
            get { return (FieldElement)BaseGet(index); }
        }
    }
}
