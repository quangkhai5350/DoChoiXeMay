using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class XuatNhapData
    {
        Model1 _context = new Model1();
        public double getTongTienAuto(int IdKy)
        {
            var Ky = _context.KyXuatNhaps.FirstOrDefault(kh => kh.Id == IdKy);
            if(Ky != null)
            {
                return Ky.TongTienAuto;
            }
            return 0;
        }
        public bool UPdateKyXN(KyXuatNhap XN)
        {
            try
            {
                _context.Entry(XN).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return false;
            }
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
        public List<ChitietXuatNhap> GetListByKy(int id)
        {
            var model = _context.ChitietXuatNhaps.Where(kh=>kh.IdKy == id)
                    .OrderByDescending(kh=>kh.NgayAuto)
                    .ToList();
            for(int i = 0; i < model.Count(); i++)
            {
                model[i].GhiChu = (i + 1).ToString();
            }
            return model;
        }
    }
}