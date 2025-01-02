using DoChoiXeMay.Areas.Admin.Data;
using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DoChoiXeMay.Utils;
using System.Data.Entity;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            ViewBag.ChiTietTC = dbc.ChiTietTCs.Where(kh => kh.AdminXacNhan == true);
            return View();
        }
        
        
        public ActionResult LogOut() {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        

    }
}