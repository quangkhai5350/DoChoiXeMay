using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class SerialData
    {
        Model1 _context;
        public SerialData()
        {
            _context = new Model1();
        }
        public bool InsertSer_sp(Ser_sp sp)
        {
            try
            {
                Ser_sp p = new Ser_sp();
                p=sp;
                p.Id = Guid.NewGuid();
                p.NgayTao = DateTime.Now;
                p.NgayUpdate = DateTime.Now;
                p.DaIn = false;
                _context.Ser_sp.Add(p);
                int kt = _context.SaveChanges();
                if (kt > 0)
                {
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
        public bool InsertSer_Box(string Lo, string Seri,bool sudung,string stt, string ghichu)
        {
            try
            {
                Ser_box b = new Ser_box();
                b.Id = Guid.NewGuid();
                b.LoSanXuat = Lo;
                b.Serial = Seri;
                b.Sudung = sudung;
                b.Stt = stt;
                b.NgayTao = DateTime.Now;
                b.NgayUpdate = DateTime.Now;
                b.DaIn = false;
                b.Ghichu = ghichu;
                _context.Ser_box.Add(b);
                int kt = _context.SaveChanges();
                if (kt > 0)
                {
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
        public bool UpdateSer_SP(string Id,bool sudung, bool dain, DateTime ngayupdate,bool tang)
        {
            try
            {
                var sp = _context.Ser_sp.Find(new Guid(Id));
                if (sp != null) { 
                    sp.Sudung = sudung;
                    sp.DaIn = dain;
                    sp.NgayUpdate= ngayupdate;
                    sp.HangTangKhongBan = tang;
                    _context.Entry(sp).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false ;
            }
            catch (Exception ex) {
                string loi = ex.ToString();
                return false;
            }
        }
        public bool UpdateSer_box(string Id, bool sudung, bool dain, DateTime ngayupdate, string ghichu)
        {
            try
            {
                var box = _context.Ser_box.Find(new Guid(Id));
                if (box != null)
                {
                    box.Sudung = sudung;
                    box.DaIn = dain;
                    box.NgayUpdate = ngayupdate;
                    box.Ghichu = ghichu;
                    _context.Entry(box).State = EntityState.Modified;
                    _context.SaveChanges();
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
        public bool InsertSer_kichhoat(string IdSer_box, string IdSer_sp, string email)
        {
            try
            {
                Ser_kichhoat kichhoat = new Ser_kichhoat();
                kichhoat.Id=Guid.NewGuid();
                kichhoat.IDSer_box = new Guid(IdSer_box);
                kichhoat.IDSer_sp = new Guid(IdSer_sp);
                kichhoat.NgayKichHoat = DateTime.Now;
                kichhoat.NgayUpdate = DateTime.Now;
                kichhoat.EmailKichHoat= email;
                kichhoat.TrangThaiId = 1;
                kichhoat.Ghichu = "";
                _context.Ser_kichhoat.Add(kichhoat);
                _context.SaveChanges();
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