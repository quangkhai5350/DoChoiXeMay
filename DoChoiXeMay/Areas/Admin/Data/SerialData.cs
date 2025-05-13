using DoChoiXeMay.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

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
                p = sp;
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
        public bool InsertSer_Box(string Lo, string Seri, bool sudung, string stt, string ghichu, string qr)
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
                b.QRcode = qr;
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
        public bool UpdateSer_SP(string Id, bool sudung, bool dain, DateTime ngayupdate, bool tang)
        {
            try
            {
                var sp = _context.Ser_sp.Find(new Guid(Id));
                if (sp != null)
                {
                    sp.Sudung = sudung;
                    sp.DaIn = dain;
                    sp.NgayUpdate = ngayupdate;
                    sp.HangTangKhongBan = tang;
                    _context.Entry(sp).State = EntityState.Modified;
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
                kichhoat.Id = Guid.NewGuid();
                kichhoat.IDSer_box = new Guid(IdSer_box);
                kichhoat.IDSer_sp = new Guid(IdSer_sp);
                kichhoat.NgayKichHoat = DateTime.Now;
                kichhoat.NgayUpdate = DateTime.Now;
                kichhoat.EmailKichHoat = email;
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
        public string getQRcode(string qrcode)
        {
            string QR = "";
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Bitmap bitMap = qRCode.GetGraphic(15))
                    {
                        bitMap.Save(ms, ImageFormat.Png);
                        QR = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
                return QR;
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return "";
            }
        }
        public string getImgtext(string value)
        {
            string kq = "";
            byte[] bytes = null;
            using (var stream = new MemoryStream())
            {
                Bitmap bitmap = new Bitmap(900, 500, PixelFormat.Format64bppArgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(System.Drawing.Color.White);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(value, new Font("Roboto", 60, FontStyle.Regular), new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)), new PointF(0.4F, 100F));
                graphics.Flush();
                graphics.Dispose();
                bitmap.Save(stream, ImageFormat.Jpeg);
                bitmap.Dispose();
                bytes = stream.ToArray();
            }
            kq = "data:image/png;base64," + Convert.ToBase64String(bytes, 0, bytes.Length);
            return kq;
        }
        public string getMergeImg(string base641, string base642)
        {
            string kq = "";
            base641 = Cleanbase64(base641);
            base642 = Cleanbase64(base642);
            Image img1 = Base64toImg(base641);
            Image img2 = Base64toImg(base642);
            //chiều rộng hình mới = tổng 2 chiều rộng
            //chiều cao hình mới = max(2 chiều cao)
            int mergewith = img1.Width + img2.Width;
            int mergeheight = Math.Max(img1.Height, img2.Height);

            Bitmap mergeImg = new Bitmap(mergewith, mergeheight);
            using (Graphics g = Graphics.FromImage(mergeImg))
            {
                g.Clear(System.Drawing.Color.White);//nen trang
                g.DrawImage(img1, 0, 0);
                g.DrawImage(img2, img1.Width, 0);
            }
            //xuat anh ra stream
            using (MemoryStream ms = new MemoryStream())
            {
                mergeImg.Save(ms, ImageFormat.Png);
                byte[] bytes = ms.ToArray();
                kq = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }
            return kq;
        }
        private string Cleanbase64(string base64)
        {
            if (base64.Contains(",")){
                return base64.Split(',')[1];
            }
            return base64;
        }
        private Image Base64toImg(string base64string)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}