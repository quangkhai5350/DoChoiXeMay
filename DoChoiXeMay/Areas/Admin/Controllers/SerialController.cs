using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    public class SerialController : Controller
    {
        // GET: Admin/Serial
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListSerialChuaIn()
        {
            return View();
        }
        public ActionResult AddNewSerial()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewSerial(Ser_sp SerSP)
        {
            return RedirectToAction("ListSerialChuaIn");
        }
    }
}