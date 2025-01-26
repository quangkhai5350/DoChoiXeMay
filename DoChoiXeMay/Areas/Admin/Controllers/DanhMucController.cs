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
                Ma.TenColor = Ma.TenColor;
                Ma.Ngay = DateTime.Now;
                Ma.Ghichu = Ma.Ghichu;
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