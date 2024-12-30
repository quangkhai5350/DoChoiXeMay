using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class XuatNhapData
    {
        Model1 dbc = new Model1();
        public double getTongTienAuto(int IdKy)
        {
            var Ky = dbc.KyXuatNhaps.FirstOrDefault(kh => kh.Id == IdKy);
            if(Ky != null)
            {
                return Ky.TongTienAuto;
            }
            return 0;
        }
    }
}