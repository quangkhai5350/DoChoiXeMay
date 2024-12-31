using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Utils
{
    public static class XString
    {
        public static String MakeAotuName()
        {
            var date = DateTime.Now;
            var str =  date.Millisecond.ToString()+date.Second.ToString()+date.Minute.ToString()+ date.Hour.ToString()
                + date.Day.ToString()+date.Month.ToString()+date.Year.ToString();
            return str;
        }
    }
}