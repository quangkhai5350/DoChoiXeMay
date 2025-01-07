using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public List<ChiTietTC> getThuChiTek(int Sec, int pageSize, string strTK,string TC, string tu, string den)
        {
            var tungay = DateTime.Now;
            var denngay = DateTime.Now;
            if (tu != "" && den != "")
            {
                tungay = DateTime.Parse(tu);
                denngay = DateTime.Parse(den);
            }
            List<ChiTietTC> model = new List<ChiTietTC>();
            if (TC== "0" && tu == "" && den == "")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .OrderByDescending(kh => kh.NgayAuto)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            else if (TC == "0" && tu != "" && den != "")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                && kh.NgayTC >= tungay && kh.NgayTC <= denngay
                && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .OrderByDescending(kh => kh.NgayAuto)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            else if (TC == "1" && tu == "" && den == "")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == true 
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .OrderByDescending(kh => kh.NgayAuto)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            else if (TC == "1" && tu != "" && den != "")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == true && kh.NgayTC >= tungay && kh.NgayTC <= denngay
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .OrderByDescending(kh => kh.NgayAuto)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            else if (TC == "2" && tu == "" && den == "")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == false 
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .OrderByDescending(kh => kh.NgayAuto)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            else if (TC == "2" && tu != "" && den != "")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == false && kh.NgayTC >= tungay && kh.NgayTC <= denngay
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                        .OrderByDescending(kh => kh.NgayAuto)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            return model;
        }
        public int GetPageCountThuChiTek(string strTK,string TC)
        {
            List<ChiTietTC> modelTC = new List<ChiTietTC>();
            strTK = strTK.ToLower().Trim();
            int model = 0;
            if (TC == "0")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                                             .Count();
            }
            else if (TC == "1")
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi==true &&(kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                                             .Count();
            }
            else
            {
                model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                        && kh.ThuChi == false && (kh.Noidung.ToLower().Contains(strTK)
                            || kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                            || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                            || kh.MaTC.TenMa.ToLower().Contains(strTK)
                            || kh.UserTek.UserName.ToLower().Contains(strTK)))
                                             .Count();
            }
                
            return model;
        }
        public Double TongbyHTvaThuChi(List<ChiTietTC> model, int IdHT,bool thuchi)
        {
            double kq = 0;
            var list = model.Where(kh => kh.IdHT == IdHT && kh.ThuChi == thuchi).ToList();
            if(list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    kq = kq + list[i].SoTien;
                }
            }
            return kq ;
        }
        public bool UPdateChiTietTC(ChiTietTC tc)
        {
            try
            {
                _context.Entry(tc).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                string loi = ex.ToString();
                return false;
            }
        }
    }
}