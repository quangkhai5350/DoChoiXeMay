using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class ActiveController : Controller
    {
        // GET: Admin/Active
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            Session["requestUri"] = "/Admin/Active/Index";
            //var model = dbc.Ser_kichhoat
            //    .OrderByDescending(kh => kh.NgayUpdate)
            //    .ThenByDescending(kh => kh.IdChiNhanh)
            //    .ThenBy(kh => kh.TrangThaiId).ToList();
            //ViewBag.TotalSerialKH = model.Count();
            ViewBag.IdChiNhanh = new SelectList(dbc.Ser_ChiNhanh.OrderBy(kh=>kh.Id), "Id", "TenChiNhanh");
            return View();
        }
        
        public ActionResult GetListDaActive(string tu, string den, int PageNo = 0, int PageSize = 8, int IdCN=0, string KeywordsTTT="")
        {
            var model = new Data.ActiveData().getSNACTek(PageNo, PageSize, IdCN,KeywordsTTT, tu, den);
            ViewBag.ListSerialKH = model;
            return PartialView(model);
        }
        public ActionResult GetPageCountActive(string tu, string den, int PageSize = 0, int IdCN=0, string KeywordsTTT = "") {
            var num = new Data.ActiveData().GetPageCountACTek(IdCN,KeywordsTTT, tu, den);
            var pageCount = Math.Ceiling(1.0 * num / PageSize);
            return Json(pageCount, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTongSanPhamAC(string tu, string den, int IdCN, string KeywordsTTT = "")
        {
            var num = new Data.ActiveData().GetPageCountACTek(IdCN,KeywordsTTT, tu, den);
            return Json(num, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSNActive(string Id)
        {
            var II= new Guid(Id);
            var model = dbc.Ser_kichhoat.Find(II);
            ViewBag.TrangThaiId = new SelectList(dbc.Ser_trangthai.OrderBy(kh => kh.Id), "Id", "Name",model.TrangThaiId);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateSNActive(Ser_kichhoat model, string ngayhethan)
        {
            return View();
        }
    }
}