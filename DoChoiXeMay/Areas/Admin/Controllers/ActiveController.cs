using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
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
            ViewBag.TotalSerialKH = dbc.Ser_kichhoat
                .OrderByDescending(kh=>kh.NgayUpdate)
                .OrderByDescending(kh=>kh.IdChiNhanh)
                .OrderBy(kh=>kh.TrangThaiId).Count();
            return View();
        }
        public ActionResult GetListDaActive()
        {
            ViewBag.ListSerialKH = dbc.Ser_kichhoat
                .OrderByDescending(kh => kh.NgayUpdate)
                .OrderByDescending(kh => kh.IdChiNhanh)
                .OrderBy(kh => kh.TrangThaiId).ToList();
            return PartialView();
        }
    }
}