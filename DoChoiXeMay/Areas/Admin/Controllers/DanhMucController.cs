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
    public class DanhMucController : Controller
    {
        // GET: Admin/DanhMuc
        Model1 dbc = new Model1();
        public ActionResult NhatKy()
        {
            Session["requestUri"] = "/Admin/DanhMuc/NhatKy";
            return View();
        }
        public ActionResult GetNhatKy()
        {
            var model = dbc.NhatKyUTeks.OrderByDescending(kh => kh.CreateDate)
                .Skip(0).Take(200).ToList();
            for (int i = 0; i < model.Count(); i++)
            {
                model[i].GhiChu = (i + 1).ToString();
            }
            ViewBag.GetNhatKy = model;
            return PartialView();
        }
        public ActionResult Levelchinhanh()
        {
            Session["requestUri"] = "/Admin/DanhMuc/Levelchinhanh";
            return View();
        }
        public ActionResult GetLevelchinhanh()
        {
            var model = dbc.Ser_Levelchinhanh.OrderByDescending(kh => kh.Id).ToList();
            ViewBag.GetLevelchinhanh = model;
            return PartialView();
        }
        
        public ActionResult UpdateLVChiNhanh(int id)
        {
            var model = dbc.Ser_Levelchinhanh.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateLVChiNhanh(Ser_Levelchinhanh LV)
        {
            try
            {
                dbc.Entry(LV).State = EntityState.Modified;
                dbc.SaveChanges();
                Session["ThongBaoLVChiNhanh"] = "Update Level chi nhánh Id=" + LV.Id + ", thành công.";
                var userid = int.Parse(Session["UserId"].ToString());
                var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Update Level chi nhánh Id=" + LV.Id + "-" + DateTime.Now.ToString(), "");
                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
                return RedirectToAction("Levelchinhanh");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ModelState.AddModelError("", "Update Thất Bại !!!!" + message);

                return View(LV);
            }
        }
        public ActionResult ListMaThuChi()
        {
            Session["requestUri"] = "/Admin/DanhMuc/ListMaThuChi";
            return View();
        }
        public ActionResult GetListMaThuChi()
        {
            var model = dbc.MaTCs.OrderByDescending(kh => kh.Id).ToList();
            ViewBag.GetListMaThuChi = model;
            return PartialView();
        }
        public ActionResult InsertMaThuChi()
        {
            MaTC model = new MaTC();
            model.TenMa = "Ma moi TeK";
            model.GhiChu = "Cần update trước khi sử dụng.";
            model.NgayAuto = DateTime.Now;
            model.SuDung = false;
            model.XuatNhap = false;
            dbc.MaTCs.Add(model);
            dbc.SaveChanges();
            Session["ThongBaoMaThuChi"] = "Insert mã thu-chi thành công. Cần Update để sử dụng.";
            var userid = int.Parse(Session["UserId"].ToString());
            var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Insert Mã Thu Chi-" + model.TenMa + "-" + DateTime.Now.ToString(), "");
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("ListThuChiTeK");
        }
        public ActionResult UpdateMaThuChi(int id)
        {
            var model = dbc.MaTCs.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMaThuChi(MaTC Ma)
        {
            try
            {
                Ma.NgayAuto = DateTime.Now;
                Ma.XuatNhap = false ;
                dbc.Entry(Ma).State = EntityState.Modified;
                dbc.SaveChanges();
                Session["ThongBaoMaThuChi"] = "Update mã thu-chi "+Ma.TenMa+" thành công.";
                var userid = int.Parse(Session["UserId"].ToString());
                var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Update mã thu chi -" + Ma.TenMa + "-" + DateTime.Now.ToString(), "");
                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
                return RedirectToAction("ListThuChiTeK");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ModelState.AddModelError("", "Update Thất Bại !!!!" + message);

                return View(Ma);
            }
        }
        public ActionResult ListMauSanPham()
        {
            Session["requestUri"] = "/Admin/DanhMuc/ListMauSanPham";
            return View();
        }
        public ActionResult GetListMauSanPham()
        {
            var model = dbc.Colors.OrderByDescending(kh => kh.Id).ToList();
            ViewBag.GetListMauSP = model;
            return PartialView();
        }
        public ActionResult InsertMauSP()
        {
            Color model = new Color();
            model.TenColor = "Mau moi TeK";
            model.Ngay=DateTime.Now;
            model.Ghichu = "Cần update trước khi sử dụng.";
            dbc.Colors.Add(model);
            dbc.SaveChanges();
            Session["ThongBaoMauSP"] = "Insert màu sp thành công. Cần Update để sử dụng.";
            var userid = int.Parse(Session["UserId"].ToString());
            var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Insert màu sp-" + model.TenColor + "-" + DateTime.Now.ToString(), "");
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("ListThuChiTeK");
        }
        public ActionResult UpdateMauSP(int id)
        {
            var model = dbc.Colors.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMauSP(Color Ma)
        {
            try
            {
                Ma.Ngay = DateTime.Now;
                dbc.Entry(Ma).State = EntityState.Modified;
                dbc.SaveChanges();
                Session["ThongBaoMauSP"] = "Update màu sp thành công.";
                var userid = int.Parse(Session["UserId"].ToString());
                var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Update màu sp-"+ Ma.TenColor+"-" + DateTime.Now.ToString(), "");
                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
                return RedirectToAction("ListThuChiTeK");
            }
            catch (Exception ex)
            {

                string message = ex.Message;
                ModelState.AddModelError("", "Update Thất Bại !!!!" + message);

                return View(Ma);
            }
        }
        public ActionResult ListHangSanXuat()
        {
            Session["requestUri"] = "/Admin/DanhMuc/ListHangSanXuat";
            return View();
        }
        public ActionResult GetListHangSanXuat()
        {
            var model = dbc.Manufacturers.OrderByDescending(kh => kh.Id).ToList();
            ViewBag.GetListHangsx = model;
            return PartialView();
        }
        public ActionResult InsertHangSX()
        {
            Manufacturer model = new Manufacturer();
            model.Name = "TeK" + DateTime.Now.ToString();
            model.Sudung = false;
            model.NgayAuto = DateTime.Now;
            model.Logo = "";
            dbc.Manufacturers.Add(model);
            dbc.SaveChanges();
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("ListThuChiTeK");
        }
        public ActionResult UpdatetHangSX(int id)
        {
            var model = dbc.Manufacturers.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdatetHangSX(Manufacturer Ma)
        {
            try
            {
                var file1 = Request.Files["Dinhkem1"];
                if (file1.ContentLength > 0)
                {
                    //Xoa hinh cu
                    bool xoahinhcu = Xstring.Xoahinhcu("imgTeK/", Ma.Logo);
                    Ma.Logo = Xstring.saveFile(file1, "imgTeK/");
                }
                Ma.NgayAuto = DateTime.Now;
                dbc.Entry(Ma).State = EntityState.Modified;
                dbc.SaveChanges();
                Session["ThongBaoHangSX"] = "Update hãng SX thành công.";
                var userid = int.Parse(Session["UserId"].ToString());
                var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Update Hãng SX-" + DateTime.Now.ToString(), "");
                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
                return RedirectToAction("ListThuChiTeK");
            }
            catch (Exception ex) { 

                string message = ex.Message;
                ModelState.AddModelError("", "Update Thất Bại !!!!"+message);

                return View(Ma);
            }
        }
    }
}