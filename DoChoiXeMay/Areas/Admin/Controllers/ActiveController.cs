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
        
        public ActionResult GetListDaActive(int PageNo = 0, int PageSize = 8, int IdCN=0, string KeywordsTTT="")
        {
            var model = new Data.ActiveData().getSNACTek(PageNo, PageSize, IdCN,KeywordsTTT);
            ViewBag.ListSerialKH = model;
            return PartialView(model);
        }
        public ActionResult GetPageCountActive(int PageSize = 20, int IdCN=0, string KeywordsTTT = "") {
            var num = new Data.ActiveData().GetPageCountACTek(IdCN,KeywordsTTT);
            var pageCount = Math.Ceiling(1.0 * num / PageSize);
            return Json(pageCount, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTongSanPhamAC(int IdCN, string KeywordsTTT = "")
        {
            var num = new Data.ActiveData().GetPageCountACTek(IdCN,KeywordsTTT);
            return Json(num, JsonRequestBehavior.AllowGet);
        }
    }
}