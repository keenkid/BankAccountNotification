using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SunGardBanMessage
    {
        private const string DTD = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

        private const string HEAD = "<head>{0}</head>";

        private const string BODY = "<body>{0}</body>";

        public SunGardMessageHeader Header { get; set; }

        public List<SunGardBanMessageBody> Body { get; set; }

        public override string ToString()
        {
            string headpart = string.Format(HEAD, Header.ToString());

            StringBuilder builder = new StringBuilder();
            foreach (SunGardBanMessageBody body in Body)
            {
                builder.Append(body.ToString());
            }
            string bodypart = string.Format(BODY, builder.ToString());
            return string.Format("{0}<root><packet>{1}{2}</packet></root>", DTD, headpart, bodypart);
        }
    }
}
