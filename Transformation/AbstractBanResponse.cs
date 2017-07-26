using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common;
using System.Reflection;
using System.Xml.Linq;
using SunGard.AvantGard.Solution.Ban.BizBase;
using SunGard.AvantGard.Solution.Ban.Formatting;

namespace SunGard.AvantGard.Solution.Ban.Transformation
{
    public abstract class AbstractBanResponse : BankResponseBase
    {
        protected SunGardBanMessage _sanm = null;

        protected List<SunGardBanMessageBody> _bodies = null;

        public AbstractBanResponse()
        {
            _sanm = new SunGardBanMessage();

            _bodies = new List<SunGardBanMessageBody>();
        }

        protected override void ParsePacket()
        {
            XDocument doc = XDocument.Parse(BankMessage);

            SunGardBanMessageBody body = new SunGardBanMessageBody();
            PropertyInfo[] props = body.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                var xmlTag = prop.GetCustomAttributes(typeof(XmlTagAttribute), false).FirstOrDefault() as XmlTagAttribute;
                if (null != xmlTag)
                {
                    var tag = xmlTag.Tag;
                    VariantElement elmt = Transaction.Response.Variants[tag];
                    string propValue = string.Empty;
                    if (!string.IsNullOrEmpty(elmt.Name))
                    {
                        if (string.IsNullOrEmpty(elmt.FieldFormat))
                        {
                            if (!string.IsNullOrEmpty(elmt.Tag))
                            {
                                propValue = doc.Descendants(elmt.Tag).FirstOrDefault().Value;
                            }
                        }
                        else
                        {
                            try
                            {
                                propValue = FormatManager.GetFormattedValue(doc, elmt.Tag, elmt.FieldFormat);
                            }
                            catch (Exception ex)
                            {
                                propValue = string.Empty;
                                Logger.ErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                            }
                        }
                    }
                    prop.SetValue(body, propValue, null);
                }
            }
            _bodies.Add(body);
        }

        protected override void PostParsePacket()
        {
            _sanm.Header = _header;
            _sanm.Body = _bodies;

            FinalMessage = _sanm.ToString();
        }
    }
}
