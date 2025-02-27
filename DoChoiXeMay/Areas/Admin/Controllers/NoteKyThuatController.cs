using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class NoteKyThuatController : Controller
    {
        // GET: Admin/NoteKyThuat
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            Session["requestUri"] = "/Admin/NoteKyThuat/Index";
            ViewBag.ViDeo = new Data.NoteKyThuatData().GetListNotebyHD(0, 1);
            ViewBag.ViDeo1 = new Data.NoteKyThuatData().Get1ListNotebyHD(0, 1);
            return View();
        }
        public ActionResult VDTEK()
        {
            Session["requestUri"] = "/Admin/NoteKyThuat/VDTEK";
            ViewBag.ViDeo = new Data.NoteKyThuatData().GetListNotebyHD(0, 2);
            ViewBag.ViDeo1 = new Data.NoteKyThuatData().Get1ListNotebyHD(0, 2);
            return View();
        }
        public ActionResult InsertVD(int loai) {
            var Max = 1;
            if (dbc.NoteKythuats.Count() > 0)
            {
                Max = dbc.NoteKythuats.OrderByDescending(kh => kh.Id).Take(1).Single().Id;
            }
            var userid = int.Parse(Session["UserId"].ToString());
            NoteKythuat model = new NoteKythuat();
            model.Id = Max + 1;
            model.NoteName = "Video New TeK.";
            model.NoiDung = "Cần update để sử dụng";
            model.SavenhieuFile = "";
            model.UserId = userid;
            model.UPush = true;
            model.PushtoNoteId = 1;
            model.AdminXacNhan = true;
            model.IdHanhDong = 0; // video=0
            model.LoaiNoteId = loai;
            dbc.NoteKythuats.Add(model);
            dbc.SaveChanges();
            Session["ThongBaoVDTEK"] = "Insert Video thành công. Cần Update để sử dụng.";
            
            var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Insert Video -" + model.NoteName + "-" + DateTime.Now.ToString(), "");
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("ListThuChiTeK");
        }
        public ActionResult DeleteVideo(int id)
        {
            var userid = int.Parse(Session["UserId"].ToString());
            var model = dbc.NoteKythuats.Find(id);
            dbc.NoteKythuats.Remove(model);
            dbc.SaveChanges();
            Session["ThongBaoVDTEK"] = "Delete Video thành công.";
            var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "Delete Video -" + model.NoteName + "-" + DateTime.Now.ToString(), "");
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditVideo(int id)
        {
            var model = dbc.NoteKythuats.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditVideo(NoteKythuat model)
        {
            try
            {
                var userid = int.Parse(Session["UserId"].ToString());
                model.UserId = userid;
                dbc.Entry(model).State = EntityState.Modified;
                dbc.SaveChanges();
                Session["ThongBaoVDTEK"] = "Update Video thành công.";
                var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                            , Session["UserName"].ToString(), "Delete Video -" + model.NoteName + "-" + DateTime.Now.ToString(), "");
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
                return View(model);
            }
            
        }
    }
}