using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
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
    }
}