using DoChoiXeMay.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public static class Xstring
    {
        public static bool Xoahinhcu(string foder, string img)
        {
            try
            {
                if(img != null && img != "")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/Areas/Admin/Content/" + foder + img)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath("~/Areas/Admin/Content/" + foder + img));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }
        public static string saveFile(HttpPostedFileBase File, string foder)
        {
            if (File.ContentLength > 0)
            {
                var ten = File.FileName;
                string[] str = ten.Split('.');

                var ext = str[str.Count() - 1].ToLower();
                if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "xls" || ext == "pdf" || ext == "xlsx"
                    || ext == "doc" || ext == "docx" || ext == "txt" || ext == "gif")
                {
                    var sub = XString.MakeAotuName();
                    ten = str[str.Count() - 2] + sub + "." + ext;
                    //Không thu nhỏ hình
                    //File.SaveAs(mapweb + foder+ten);
                    File.SaveAs(HttpContext.Current.Server.MapPath("~/Areas/Admin/Content/"+ foder+ten));
                }
                else ten = "";
                return ten;
            }
            return "";
        }
    }
}