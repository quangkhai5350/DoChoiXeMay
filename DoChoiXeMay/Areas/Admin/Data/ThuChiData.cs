using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
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
        public List<ChiTietTC> getThuChiTek(int Sec, int pageSize, string strTK)
        {
            ////lọc theo hình thức, maxTC, Kỳ   để sau
            var modle = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                    && (kh.Noidung.ToLower().Contains(strTK)||kh.HinhThucTC.TenHT.ToLower().Contains(strTK)
                        || kh.KyXuatNhap.TenKy.ToLower().Contains(strTK)
                        || kh.MaTC.TenMa.ToLower().Contains(strTK)))
                    .OrderByDescending(kh => kh.NgayAuto)
                    .Skip(Sec * pageSize)
                    .Take(pageSize)
                    .ToList();
            
            return modle;
        }
        public int GetPageCountThuChiTek(string strTK)
        {
            ////lọc theo hình thức, maxTC, Kỳ   để sau
            int model = 0;
            model = _context.ChiTietTCs.Where(kh => kh.AdminXacNhan == true && kh.YeuCauDay == true
                    && kh.Noidung.ToLower().Contains(strTK))
                                         .Count();
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
    }
}