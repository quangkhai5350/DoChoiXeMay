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
            var date = DateTime.Now.ToString("dd/MM/yy");
            string[] str1 = date.Split('/');
            string d=str1[0];
            string m=str1[1];
            string y=str1[2];
            string str = d+m+y+ GetRanDomOTP(i);
            
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