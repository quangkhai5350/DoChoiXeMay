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
        Model1 dbc = new Model1();
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
            ViewBag.IDMF = new SelectList(dbc.Manufacturers.Where(kh => kh.Sudung == true), "Id", "Name",5);
            ViewBag.IdColor = new SelectList(dbc.Colors.ToList(), "Id", "TenColor");
            ViewBag.IdSize = new SelectList(dbc.Sizes.ToList(), "Id", "TenSize");
            ViewBag.Idver = new SelectList(dbc.Versions.ToList(), "Id", "VerName");
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