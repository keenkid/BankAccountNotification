using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public static class StringExtension
    {
        public static string GbkSubString(this string str, int startIndex, int length)
        {
            byte[] bwrite = Encoding.GetEncoding("gb2312").GetBytes(str.ToCharArray());
            return Encoding.Default.GetString(bwrite, startIndex, length);
        }
    }
}
