using DoChoiXeMay.Areas.Admin.Data;
using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class XuatNhapController : Controller
    {
        // GET: Admin/XuatNhap
        Model1 dbc=new Model1();
        public ActionResult ListXuatNhapUser()
        {
            ViewBag.IdMaTC = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true && kh.XuatNhap==true), "Id", "GhiChu");
            return View();
        }
        public ActionResult GetListKyXNUser()
        {
            var uid = int.Parse(Session["UserId"].ToString());
            if(Session["quyen"].ToString() == "Admin")
            {
                ViewBag.KyXN = dbc.KyXuatNhaps.Where(kh => kh.UserId == uid && kh.Id > 1 && kh.UPush == false)
                    .OrderByDescending(kh => kh.Id)
                    .ToList();
            }
            else
            {
                ViewBag.KyXN = dbc.KyXuatNhaps.Where(kh => kh.UserId == uid && kh.Id > 1 && kh.AdminXNPUSH == false)
                    .OrderByDescending (kh => kh.Id)
                    .ToList();
            }
            
            return PartialView();
        }
        [HttpGet]
        public ActionResult UpdateKyXNUser(int id)
        {
            var model = dbc.KyXuatNhaps.Find(id);
            ViewBag.IdMaTC = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true && kh.XuatNhap == true), "Id", "GhiChu",model.IdMaTC);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateKyXNUser(KyXuatNhap XN)
        {
            try
            {
                var NgayXuatNhap = XN.NgayXuatNhap.ToString("{dd/MM/yyyy}");
                var file1 = Request.Files["Dinhkem1"];
                var file2 = Request.Files["Dinhkem2"];
                var file3 = Request.Files["Dinhkem3"];
                if (file1 != null)
                {
                    //Xoa hinh cu
                    bool xoahinhcu = Xstring.Xoahinhcu("imgxuatnhap/", XN.HoaDon);
                    XN.HoaDon = Xstring.saveFile(file1, "imgxuatnhap/");
                }
                if (file2 != null)
                {
                    //Xoa hinh cu
                    bool xoahinhcu = Xstring.Xoahinhcu("imgxuatnhap/", XN.Filesave2);
                    XN.Filesave2 = Xstring.saveFile(file2, "imgxuatnhap/");
                }
                if (file3 != null)
                {
                    //Xoa hinh cu
                    bool xoahinhcu = Xstring.Xoahinhcu("imgxuatnhap/", XN.Filesave3);
                    XN.Filesave3 = Xstring.saveFile(file3, "imgxuatnhap/");
                }
                XN.NgayAuto = DateTime.Now;
                var kq = new Data.XuatNhapData().UPdateKyXN(XN);
                if (kq == true)
                {
                    var userid = int.Parse(Session["UserId"].ToString());
                    Session["ThongBaoXuatNhapUser"] = "Update thanh cong kỳ xuất nhập ngay: " + NgayXuatNhap;
                    var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "UpdateKyXNUser-" + NgayXuatNhap, "");
                    return RedirectToAction("ListXuatNhapUser");
                }
                else
                {
                    ModelState.AddModelError("", "Update Kỳ XN Thất Bại !!!!");
                    var model = dbc.ChiTietTCs.Find(XN.Id);
                    ViewBag.IdMaTC = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true && kh.XuatNhap == true), "Id", "GhiChu", XN.IdMaTC);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                ModelState.AddModelError("", "Update Xuat Nhập Thất Bại !!!! Có Lỗi hệ thống.");
                var model = dbc.ChiTietTCs.Find(XN.Id);
                ViewBag.IdMaTC = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true && kh.XuatNhap == true), "Id", "GhiChu", XN.IdMaTC);
                return View(model);
            }
            
        }
        public ActionResult InsertKyXuatNhap(KyXuatNhap XN)
        {
            try
            {
                var uid = int.Parse(Session["UserId"].ToString());
                KyXuatNhap model = new KyXuatNhap();
                model = XN;
                model.UserId = uid;
                model.UPush = false;
                if (Session["quyen"].ToString() == "Admin")
                {
                    model.AdminXNPUSH = true;
                }
                else
                {
                    model.AdminXNPUSH = false;
                }
                model.UYeuCauXoa = false;
                model.TongTienAuto = 0;
                model.NgayAuto = DateTime.Now;
                var file1 = Request.Files["HoaDon"];
                var file2 = Request.Files["Filesave2"];
                var file3 = Request.Files["Filesave3"];
                var ten1 = Xstring.saveFile(file1, "imgxuatnhap/");
                var ten2 = Xstring.saveFile(file2, "imgxuatnhap/");
                var ten3 = Xstring.saveFile(file3, "imgxuatnhap/");

                model.HoaDon = ten1;
                model.Filesave2 = ten2;
                model.Filesave3 = ten3;
                dbc.KyXuatNhaps.Add(model);
                int kt = dbc.SaveChanges();
                if (kt > 0)
                {
                    Session["ThongBaoXuatNhapUser"] = "Thêm mới thành công Kỳ xuất nhập ngày:" + model.NgayXuatNhap.ToString("{dd/MM/yyyy}");
                    var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, uid, Session["quyen"].ToString(),
                         Session["UserName"].ToString(), "Insert thành công kỳ XN:" + model.NgayXuatNhap.ToString("{dd/MM/yyyy}"), "");
                    return RedirectToAction("ListXuatNhapUser");
                }
                else
                {
                    Session["ThongBaoXuatNhapUser"] = "Thêm mới thất bại Kỳ xuất nhập ngày:" + model.NgayXuatNhap.ToString("{dd/MM/yyyy}") + " !!!";
                    return RedirectToAction("ListXuatNhapUser");
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                Session["ThongBaoXuatNhapUser"]="Thêm mới Thất Bại !!!!!!!!!!. Có lỗi hệ thống";
            }
            
            return RedirectToAction("ListXuatNhapUser");
        }
        [HttpGet]
        public ActionResult InsertChiTietXNbyKy(int id) {
            var ky = dbc.KyXuatNhaps.Find(id);
            Session["TenKy"]=ky.TenKy;
            Session["IDKy"] = id;
            Session["CKphantram"] = ky.CKphantram;
            Session["CKtienmat"] = ky.CKtienmat;
            ViewBag.IDMF = new SelectList(dbc.Manufacturers.Where(kh => kh.Sudung == true), "Id", "Name");
            ViewBag.IDColor = new SelectList(dbc.Colors.OrderByDescending(kh => kh.Id), "Id", "TenColor");
            ViewBag.IDSize = new SelectList(dbc.Sizes.OrderBy(kh => kh.Id), "Id", "TenSize");
            return View();
        }
        [HttpPost]
        public ActionResult InsertChiTietXNbyKy(ChitietXuatNhap ctxn)
        {

            ChitietXuatNhap model = new ChitietXuatNhap();
            model = ctxn;
            model.Id = Guid.NewGuid();
            model.IdKy = int.Parse(Session["IDKy"].ToString());
            model.NgayAuto = DateTime.Now;
            var file1 = Request.Files["Hinh1"];
            var file2 = Request.Files["Hinh2"];
            var file3 = Request.Files["Hinh3"];
            var ten1 = Xstring.saveFile(file1, "imgxuatnhap/");
            var ten2 = Xstring.saveFile(file2, "imgxuatnhap/");
            var ten3 = Xstring.saveFile(file3, "imgxuatnhap/");
            model.Hinh1 = ten1;
            model.Hinh2 = ten2;
            model.Hinh3 = ten3;
            dbc.ChitietXuatNhaps.Add(model);
            int kq= dbc.SaveChanges();
            if (kq > 0)
            {
                var tt =  TinhTongtienKy(model.IdKy);
                KyXuatNhap XN = dbc.KyXuatNhaps.Find(model.IdKy);
                XN.TongTienAuto = tt;
                dbc.Entry(XN).State = EntityState.Modified;
                var update=dbc.SaveChanges();
            }
            return RedirectToAction("ListXuatNhapUser");
        }
        public double TinhTongtienKy(int id)
        {
            double kq=0;
            var ky = dbc.KyXuatNhaps.Find(id);
            var model = dbc.ChitietXuatNhaps.Where(kh => kh.IdKy == id).ToList();
            
                for (int i = 0; i < model.Count(); i++)
                {
                    double tong = model[i].SoLuong * model[i].Gianhap;
                    double tongtien = tong - model[i].CKtienmat - model[i].CKphantram * tong/100;
                    kq = kq + tong;
                }
            return kq + ky.Shipper + ky.VAT * kq/100;
        }
    }
}