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
            Session["UserName"] = "Admin";
            Session["UserId"] = "1";
            Session["quyen"] = "Admin";
            return View();
        }
        [Protect]
        [HttpGet]
        public ActionResult InsertThuChi()
        {
            var model = new ChiTietTC();
            ViewBag.IdMa = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true), "Id", "TenMa");
            //dbc.MaTCs.Select(x => new SelectListItem { Text = x.TenMa, Value = x.Id.ToString() }).ToList();

            //new SelectList(dbc.MaTCs.Where(kh=>kh.SuDung==true),"Id", "TenMa",0);
            ViewBag.IdHT = new SelectList(dbc.HinhThucTCs.Where(kh => kh.SuDung == true), "Id", "TenHT");
            //dbc.HinhThucTCs.Select(x => new SelectListItem { Text = x.TenHT, Value = x.Id.ToString() }).ToList();

            //new SelectList(dbc.HinhThucTCs.Where(kh => kh.SuDung == true), "Id", "TenHT",0);

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertThuChikh(ChiTietTC TC)
        {
            ViewBag.IdHT = new SelectList(dbc.HinhThucTCs.Where(kh => kh.SuDung == true), "Id", "TenHT");
            ViewBag.IdMa = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true), "Id", "TenMa");
            try
            {
                ChiTietTC p = new ChiTietTC();
                p.Id = Guid.NewGuid();
                p.IdMa = TC.IdMa;
                p.IdHT = TC.IdHT;
                p.IdKyxuatnhap = TC.IdKyxuatnhap;
                p.Noidung = TC.Noidung;
                p.ThuChi = TC.ThuChi;
                p.Ghichu = TC.Ghichu;
                p.Filesave1 = "";
                p.Filesave2 = "";
                p.Filesave3 = "";
                p.NgayTC = TC.NgayTC;
                p.NgayAuto = DateTime.Now;
                p.SoTien = double.Parse(TC.SoTien.ToString());
                p.UserId = int.Parse(Session["UserId"].ToString());
                p.AdminXacNhan = false;
                dbc.ChiTietTCs.Add(p);
                int kt = dbc.SaveChanges();
                if(kt > 0)
                {
                    var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, p.UserId, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "InsertThuChi-" + p.NgayTC, "");
                    Session["ThongBao_InsertTC"] = "Insert thành công File Thu Chi Ngày: " + p.NgayTC;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                ModelState.AddModelError("", "Thêm mới Thất Bại !!!!!!!!!!. Có lỗi hệ thống");
                return View("InsertThuChi");
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