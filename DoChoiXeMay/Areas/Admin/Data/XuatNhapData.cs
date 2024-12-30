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
        public static bool InsertNhatKy_Admin(Model1 dbc, int UserID, string LoaiUser, string UserName, string CongViec, string GhiChu)
        {
            try
            {
                if (LoaiUser != "Guest")
                {
                    NhatKyUTek model = new NhatKyUTek();
                    model.Id = Guid.NewGuid();
                    model.UserID = UserID;
                    model.UserName = UserName;
                    model.LoaiUser = LoaiUser;
                    model.CreateDate = DateTime.Now;
                    model.CongViec = CongViec;
                    model.GhiChu = GhiChu;
                    dbc.NhatKyUTeks.Add(model);
                    dbc.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return false;
            }

        }
    }
}