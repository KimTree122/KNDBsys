using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common
{
    public static class TypeChange
    {
        public static int ToInt(this string t)
        {
            if (string.IsNullOrWhiteSpace(t)) return 0;
            int.TryParse(t, out int i);
            return i;
        }

        public static DateTime StrToDateTime(this string str)
        {
            DateTime dtime = DateTime.Now;
            if (string.IsNullOrWhiteSpace(str)) return dtime;
            DateTime.TryParse(str, out dtime);
            return dtime ;
        }

        public static string DecDES(this string str)
        {
            return Secret_string.DecryptDES(str);
        }
    }
}
