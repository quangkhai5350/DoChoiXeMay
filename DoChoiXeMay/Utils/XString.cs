using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static String MakeAotuSN(int i)
        {
            var date = DateTime.Now;
            string m = date.Millisecond.ToString();
            string s = date.Second.ToString();
            var str = m + s + GetRanDomOTP(5);
            if(str.Length < i)
            {
                int kk=i-str.Length;
                str = GetRanDomOTP(kk)+ m + s + GetRanDomOTP(5);
            }
            return str;
        }
        public static string GetRanDomOTP(int i)
        {
            //get Random text
            StringBuilder randomText = new StringBuilder();
            string alphabets = "123456789QWERTYUIPASDFGHJKLZXCVBNM#@&*";
            Random r = new Random();
            for (int j = 0; j < i; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }

            string text = randomText.ToString();
            return text;
        }
    }
}