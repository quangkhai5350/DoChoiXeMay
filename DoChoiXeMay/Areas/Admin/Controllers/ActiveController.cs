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
            var model = dbc.Ser_kichhoat
                .OrderByDescending(kh => kh.NgayUpdate)
                .OrderByDescending(kh => kh.IdChiNhanh)
                .OrderBy(kh => kh.TrangThaiId).ToList();
            ViewBag.TotalSerialKH = model.Count();
            return View();
        }
        public ActionResult GetListDaActive()
        {
            var model = dbc.Ser_kichhoat
                .OrderByDescending(kh=>kh.IdChiNhanh)
                .ThenByDescending(kh => kh.NgayUpdate)
                .ThenBy(kh => kh.TrangThaiId).ToList();
            ViewBag.ListSerialKH = model;
            return PartialView(model);
        }
    }
}