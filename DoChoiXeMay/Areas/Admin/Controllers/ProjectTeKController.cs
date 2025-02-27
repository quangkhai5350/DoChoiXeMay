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
        public ActionResult InsertProJectTeK() {
            var userid = int.Parse(Session["UserId"].ToString());
            ProjectTeK model = new ProjectTeK();
            model.NameProject = "New ProjectTek " + DateTime.Now;
            model.Giaithich = "Giải thích là kiểu bài tập làm văn có nhiệm vụ trình bày, giảng giải, cắt nghĩa về nội dung, lí do, quy luật, ý nghĩa của các hiện tượng, sự vật và lời nói của con người.";
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now.AddMonths(3);
            model.Uutien = true;
            model.TrangthaiId = 1;
            model.GroupId = 1;
            model.PhantramHoanThanh = 0;
            model.NgayCapNhat = DateTime.Now;
            model.GhiChu = "";
            model.UserId = userid;
            dbc.ProjectTeKs.Add(model);
            int kt = dbc.SaveChanges();
            if (kt > 0)
            {
                Session["ThongBaoProject"] = "Insert thành công Dự Án mới, bạn phải update để sử dụng.";
            }
            else
            {
                Session["ThongBaoProject"] = "Insert Dự Án mới thất bại !!!.";
            }
            var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Insert Dự Án -" + model.NameProject + "-" + DateTime.Now.ToString(), "");
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProject(int id)
        {
            var userid = int.Parse(Session["UserId"].ToString());
            var model = dbc.ProjectTeKs.Find(id);
            dbc.ProjectTeKs.Remove(model);
            dbc.SaveChanges();
            Session["ThongBaoProject"] = "Delete Dự Án "+ model.NameProject +" thành công.";
            var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Delete Dự Án -" + model.NameProject + "-" + DateTime.Now.ToString(), "");
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateProjectTeK(int id)
        {

            var model = dbc.ProjectTeKs.Find(id);
            ViewBag.TrangthaiId = new SelectList(dbc.TrangThaiDuAns.ToList(), "Id", "Name", model.TrangthaiId);
            ViewBag.User = dbc.UserTeks.OrderBy(kh=>kh.Id).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProjectTeK(ProjectTeK teK)
        {
            try
            {

                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ModelState.AddModelError("", "Update Thất Bại !!!!" + message);
                return View(teK);
            }
            
        }
    }
}