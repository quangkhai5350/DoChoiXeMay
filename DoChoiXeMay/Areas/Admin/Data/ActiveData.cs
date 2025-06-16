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
        public List<Ser_kichhoat> getSNACTek(int Sec, int pageSize, int IdCN)
        {
            List<Ser_kichhoat> model1 = new List<Ser_kichhoat>();
            if(IdCN == 0)
            {
                model1 = _context.Ser_kichhoat
                    .OrderByDescending(kh => kh.NgayUpdate)
                    .ThenByDescending(kh => kh.IdChiNhanh)
                    .ThenBy(kh => kh.TrangThaiId)
                    .Skip(Sec * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                model1 = _context.Ser_kichhoat.Where(kh => kh.IdChiNhanh == IdCN)
                    .OrderByDescending(kh => kh.NgayUpdate)
                    .ThenByDescending(kh => kh.IdChiNhanh)
                    .ThenBy(kh => kh.TrangThaiId)
                    .Skip(Sec * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            

            //for (int i = 0; i < model1.Count(); i++)
            //{
            //    model1[i].STT = (i + 1).ToString();
            //}

            //model1 = model1
            //    .OrderByDescending(kh => kh.NgayAuto)
            //    .Skip(Sec * pageSize)
            //                .Take(pageSize)
            //                .ToList();
            return model1;
        }
        public int GetPageCountACTek(int IdCN)
        {
            var model1 = 0;
            if (IdCN==0)
            {
                model1 = _context.Ser_kichhoat.Count();
            }
            else
            {
                model1 = _context.Ser_kichhoat.Where(kh => kh.IdChiNhanh == IdCN).Count();
            }
            
            return model1;
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
        public string InsertUserAotu()
        {
            UserTek model = new UserTek();
            var name = "TeK" + (_context.UserTeks.Count() + 1000).ToString();
            var checkname = _context.UserTeks.FirstOrDefault(kh => kh.UserName == name);
            if (checkname != null)
            {
                //Nếu name bị trùng, random 100 lần
                for (int j = 0; j < 100; j++)
                {
                    name = "TeK" + (_context.UserTeks.Count() + 1000 * (j + 2)).ToString();
                    checkname = _context.UserTeks.FirstOrDefault(kh => kh.UserName == name);
                    if (checkname == null)
                    {
                        model.UserName = name;
                        break;
                    }
                }
            }else
            {
                model.UserName = name;
            }
            model.Password = "4+szJJPdHNwGTpohvWoq5W0FS0TGKrNhny2zvF6cf64fgvm9EvAuew==";
            model.PasswordSalt = "cwYRNpQl/Jissz6PZo/oUjHBEsYJw8w=";
            model.IdLoai = 5;
            model.LoaiConnection = "";
            model.EmailConnection = "email@gmail.com";
            model.Islocked = false;
            model.lastPasswordChangedate = DateTime.Now;
            model.LastLokedChangedate = DateTime.Now;
            model.Createdate = DateTime.Now;
            model.CountFailedPassword = 0;
            model.GhiChu = "";
            model.Avatar = "";
            _context.UserTeks.Add(model);
            var kq= _context.SaveChanges();
            if (kq > 0)
            {
                return model.UserName;
            }
            return "No";
        }
    }
}