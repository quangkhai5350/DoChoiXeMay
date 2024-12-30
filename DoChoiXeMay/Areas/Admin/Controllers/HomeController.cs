using DoChoiXeMay.Areas.Admin.Data;
using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            Session["User"] = "Admin";
            Session["UserId"] = 1;
            Session["quyen"] = "Admin";
            return View();
        }
        [Protect]
        public ActionResult InsertThuChi()
        {
            ViewBag.IdMa = new SelectList(dbc.MaTCs.Where(kh=>kh.SuDung==true).ToList(),"Id", "TenMa");
            ViewBag.IdHT = new SelectList(dbc.HinhThucTCs.Where(kh => kh.SuDung == true).ToList(), "Id", "TenHT");
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertThuChi(ChiTietTC TC)
        {
            try
            {
                ChiTietTC p = new ChiTietTC();
                p.Id = Guid.NewGuid();
                p.SoTien = TC.SoTien;
                p.ThuChi = TC.ThuChi;
                p.Noidung = TC.Noidung;
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                ModelState.AddModelError("", "Thêm mới Thất Bại !!!!!!!!!!. Có lỗi hệ thống");
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult LoadKyXuatNhap()
        {
           var Kyxuatnhap = dbc.KyXuatNhaps.Where(kh => kh.AdminXacNhan == true && kh.Id>1
                && dbc.ChiTietTCs.FirstOrDefault(nn => nn.IdKyxuatnhap == kh.Id) == null)
                    .OrderByDescending(kh => kh.NgayAuto).Select(kh => new
                    {
                        Id=kh.Id,
                        TenKy = kh.TenKy
                    });
            return Json(Kyxuatnhap, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getTongTien(int Id)
        {
            var tong = new XuatNhapData().getTongTienAuto(Id);
            return Json(tong, JsonRequestBehavior.AllowGet);
        }
    }
}