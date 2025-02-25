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
    public class ProjectTeKController : Controller
    {
        // GET: Admin/ProjectTeK
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            Session["requestUri"] = "/Admin/ProjectTeK/Index";
            ViewBag.ListProJect = new Data.ProjectTeKData().GetListProjectTeK();
            return View();
        }
    }
}