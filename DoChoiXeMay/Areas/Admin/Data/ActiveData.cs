using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class ActiveData
    {
        Model1 _context;
        public ActiveData() { 
            _context = new Model1();
        }
        public bool InsertKichHoatBH(bool ND, string IdBox, string IdSP,int chinhanh,string email, string tenkh,string sdt)
        {
            try
            {
                if (ND)
                {
                    Ser_kichhoat kh = new Ser_kichhoat();
                    kh.Id = Guid.NewGuid();
                    kh.IDSer_box = new Guid(IdBox);
                    kh.IDSer_sp=new Guid(IdSP);
                    kh.NgayKichHoat =   DateTime.Now;
                    kh.NgayUpdate = DateTime.Now;
                    kh.EmailKichHoat = email;
                    kh.TenKhachHang = tenkh;
                    kh.SDT = sdt;
                    kh.IdChiNhanh = chinhanh;
                    kh.TrangThaiId = 1;
                    kh.Ghichu = DateTime.Now.AddMonths(12).ToShortDateString();
                    _context.Ser_kichhoat.Add(kh);
                    int kt = _context.SaveChanges();
                    if (kt > 0)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    Ser_kichhoat kh = new Ser_kichhoat();
                    kh.Id = Guid.NewGuid();
                    kh.IDSer_box = new Guid(IdBox);
                    kh.IDSer_sp = new Guid(IdSP);
                    kh.NgayKichHoat = DateTime.Now;
                    kh.NgayUpdate = DateTime.Now;
                    kh.EmailKichHoat = email;
                    kh.TenKhachHang = tenkh;
                    kh.SDT = sdt;
                    kh.IdChiNhanh = chinhanh;
                    kh.TrangThaiId = 1;
                    kh.Ghichu = "Nhà phân phối, đại lý";
                    _context.Ser_kichhoat.Add(kh);
                    int kt = _context.SaveChanges();
                    if (kt > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return false;
            }
        }
    }
}