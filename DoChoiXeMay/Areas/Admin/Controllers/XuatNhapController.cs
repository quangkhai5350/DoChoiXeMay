using DoChoiXeMay.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class XuatNhapController : Controller
    {
        // GET: Admin/XuatNhap
        public ActionResult ListXuatNhapUser()
        {

            return View();
        }
        public ActionResult InsertKyXuatNhap()
        {

            return RedirectToAction("ListXuatNhapUser");
        }
    }
}