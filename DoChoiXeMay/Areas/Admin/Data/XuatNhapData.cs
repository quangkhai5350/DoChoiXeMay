using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

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
        public List<KyXuatNhap> getXuatNhapTek(int Sec, int pageSize)
        {
            var model1 = _context.KyXuatNhaps.Where(kh => kh.Id > 1 && kh.AdminXNPUSH == true && kh.UPush == true)
                    .OrderBy(kh => kh.NgayAuto)
                    .ToList();
            for (int i = 0; i < model1.Count(); i++)
            {
                model1[i].STT = (i +1).ToString();
            }

            model1 = model1
                .OrderByDescending(kh => kh.NgayAuto)
                .Skip(Sec * pageSize)
                            .Take(pageSize)
                            .ToList();
            return model1;
        }
        public int GetPageCountXuatNhapTek()
        {
            var model1 = _context.KyXuatNhaps.Where(kh => kh.Id > 1 && kh.AdminXNPUSH == true && kh.UPush == true).Count();

            return model1;
        }
        public static bool InsertMsgAotu(Model1 dbc,int UserId, string MsgSys,bool AdminDaxem, bool Sub2Daxem,bool Sub4Daxem,bool Sub5Daxem,bool Sub6Daxem)
        {
            try
            {
                MsgAotu model = new MsgAotu();
                model.Id = Guid.NewGuid();
                model.UserIdmsgAotu = UserId;
                model.MsgHeThong = MsgSys;
                model.NgayTao = DateTime.Now;
                model.AdminDaxem = AdminDaxem;
                model.Sub2Daxem = Sub2Daxem;
                model.Sub4Daxem = Sub4Daxem;
                model.Sub5Daxem = Sub5Daxem;
                model.Sub6Daxem = Sub6Daxem;
                dbc.MsgAotus.Add(model);
                dbc.SaveChanges();
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