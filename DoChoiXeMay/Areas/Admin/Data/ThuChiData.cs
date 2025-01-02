using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
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
                    && kh.Noidung.ToLower().Contains(strTK) )
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
    }
}