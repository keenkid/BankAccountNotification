using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunGard.AvantGard.TaskSchedulerControl
{
    class ControlElementCollection : ConfigurationElementCollection
    {
        private const string ELMTNAME = "control";

        protected override string ElementName
        {
            get
            {
                return ELMTNAME;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ControlElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ControlElement)element).BankCode;
        }

        public new ControlElement this[string bankCode]
        {
            get
            {
                return base.BaseGet(bankCode) as ControlElement;
            }
        }

        public ControlElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as ControlElement;
            }
        }
    }
}
