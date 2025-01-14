using DoChoiXeMay.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class ThuChiData
    {
        Model1 _context;
        public ThuChiData()
        {
            _context = new Model1();
        }
        public static List<ChiTietTC> ChiTietTCDBTEK(Model1 db, string strTK, string TC, string tu, string den)
        {
            var tungay = DateTime.Now;
            var denngay = DateTime.Now;
            if (tu != "" && den != "")
            {
                tungay = DateTime.Parse(tu);
                denngay = DateTime.Parse(den);
            }
            List<ChiTietTC> model = new List<ChiTietTC>();
            if (TC == "0" && tu == "" && den == "")
            {
                model = db.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .ToList();
            }
            else if (TC == "0" && tu != "" && den != "")
            {
                model = db.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                && kh.NgayTC >= tungay && kh.NgayTC <= denngay
                && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .ToList();
            }
            else if (TC == "1" && tu == "" && den == "")
            {
                model = db.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == true
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .ToList();
            }
            else if (TC == "1" && tu != "" && den != "")
            {
                model = db.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == true && kh.NgayTC >= tungay && kh.NgayTC <= denngay
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .ToList();
            }
            else if (TC == "2" && tu == "" && den == "")
            {
                model = db.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == false
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .ToList();
            }
            else if (TC == "2" && tu != "" && den != "")
            {
                model = db.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == false && kh.NgayTC >= tungay && kh.NgayTC <= denngay
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .ToList();
            }
            return model;
        }
        public List<ChiTietTC> getThuChiTek(int Sec, int pageSize, string strTK, string TC, string tu, string den)
        {
            var model1 = ChiTietTCDBTEK(_context, strTK, TC, tu, den)
                            .OrderBy(kh => kh.NgayAuto).ToList();


            for (int i = 0; i < model1.Count(); i++)
            {
                model1[i].STT = i.ToString();
            }

            model1 = model1
                .OrderByDescending(kh => kh.NgayAuto)
                .Skip(Sec * pageSize)
                            .Take(pageSize)
                            .ToList();

            return model1;
        }
        public int GetPageCountThuChiTek(string strTK, string TC, string tu, string den)
        {
            var model1 = ChiTietTCDBTEK(_context, strTK, TC, tu, den).Count();

            return model1;
        }
        public Double TongbyHTvaThuChi(List<ChiTietTC> model, int IdHT, bool thuchi)
        {
            double kq = 0;
            var list = model.Where(kh => kh.IdHT == IdHT && kh.ThuChi == thuchi).ToList();
            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    kq = kq + list[i].SoTien;
                }
            }
            return kq;
        }
        public bool UPdateChiTietTC(ChiTietTC tc)
        {
            try
            {
                _context.Entry(tc).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return false;
            }
        }
        public bool InsertThuChiTeK(ChiTietTC TC, string quyen,string UserName)
        {
            try
            {
                ChiTietTC p = new ChiTietTC();
                p = TC;
                p.Id = Guid.NewGuid();
                p.NgayAuto = DateTime.Now;
                if (quyen == "Admin")
                {
                    // nếu Session["quyen"]=="Admin" thì đẩy thẳng lên Tek
                    p.YeuCauDay = true;
                    p.AdminXacNhan = true;
                }
                else
                {
                    p.YeuCauDay = false;
                    p.AdminXacNhan = false;
                }
                _context.ChiTietTCs.Add(p);
                int kt = _context.SaveChanges();
                if (kt > 0)
                {
                    var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(_context, p.UserId, quyen
                        , UserName, "InsertThuChi-" + p.NgayTC, "");
                    return true;
                }
                return false;
                    
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return false;
            }
        }
    }
}