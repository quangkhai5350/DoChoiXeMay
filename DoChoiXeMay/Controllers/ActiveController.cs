using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Controllers
{
    public class ActiveController : Controller
    {
        // GET: Active
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            //(bool)Session["Pay"] = true
            if (Session["ND"]==null && Session["NPP"] == null)
            {
                ViewBag.ND = "ND";
                ViewBag.NPP = "NONPP";
            }
            else
            {
                ViewBag.ND = Session["ND"].ToString();
                ViewBag.NPP = Session["NPP"].ToString();
            }
            ViewBag.IdChiNhanh = new SelectList(dbc.Ser_ChiNhanh.Where(kh => kh.Sudung == true), "Id", "TenChiNhanh");
            return View();
        }
    }
}