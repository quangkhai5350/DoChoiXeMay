using DoChoiXeMay.Areas.Admin.Data;
using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using DoChoiXeMay.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class ThuChiController : Controller
    {
        // GET: Admin/ThuChi
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListThuChiUser()
        {
            var uid = int.Parse(Session["UserId"].ToString());
            if (Session["quyen"].ToString() == "Admin")
            {
                //AdminXacNhan chưa xác nhận và User có yêu cầu đẩy
                ViewBag.ChitietTCUser = dbc.ChiTietTCs.Where(kh => kh.AdminXacNhan == false
            && kh.YeuCauDay == true).OrderByDescending(kh => kh.NgayAuto);
            }
            else if (Session["quyen"].ToString() == "SubAdmin")
            {
                //AdminXacNhan chưa xác nhận
                ViewBag.ChitietTCUser = dbc.ChiTietTCs.Where(kh => kh.AdminXacNhan == false
            && kh.UserId == uid).OrderByDescending(kh => kh.NgayAuto);
            }

            return View();
        }
        public ActionResult ListThuChiTeK()
        {

            return View();
        }
        public ActionResult GetListThuChiTek(string strk, int PageNo = 0, int PageSize = 8)
        {
            //var uid = int.Parse(Session["UserId"].ToString());
            ViewBag.ChitietTCTEK = new Data.ThuChiData().getThuChiTek(PageNo,PageSize,strk);
            return PartialView();
        }
        public ActionResult GetPageCountThuChiTek(string Keyword, int PageSize = 8)
        {
            var num = new Data.ThuChiData().GetPageCountThuChiTek(Keyword);
            var pageCount = Math.Ceiling(1.0 * num / PageSize);
            return Json(pageCount, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult InsertThuChi()
        {
            var model = new ChiTietTC();
            ViewBag.IdMa = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true), "Id", "GhiChu");

            ViewBag.IdHT = new SelectList(dbc.HinhThucTCs.Where(kh => kh.SuDung == true), "Id", "TenHT");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertThuChikh(ChiTietTC TC)
        {
            ViewBag.IdHT = new SelectList(dbc.HinhThucTCs.Where(kh => kh.SuDung == true), "Id", "TenHT");
            ViewBag.IdMa = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true), "Id", "TenMa");
            try
            {
                ChiTietTC p = new ChiTietTC();
                p = TC;
                p.Id = Guid.NewGuid();

                var file1 = Request.Files["Filesave1"];
                var file2 = Request.Files["Filesave2"];
                var file3 = Request.Files["Filesave3"];
                var ten1 = saveFile_imgthuchi(file1);
                var ten2 = saveFile_imgthuchi(file2);
                var ten3 = saveFile_imgthuchi(file3);

                p.Filesave1 = ten1;
                p.Filesave2 = ten2;
                p.Filesave3 = ten3;

                p.NgayAuto = DateTime.Now;
                p.UserId = int.Parse(Session["UserId"].ToString());
                if (Session["quyen"].ToString() == "Admin")
                {
                    // nếu Session["quyen"]=="Admin" thì đẩy thẳng lên Tek
                    p.YeuCauDay = true;
                    p.AdminXacNhan = true;
                }
                else
                {
                    p.YeuCauDay = false;
                    p.AdminXacNhan = false;
                }
                dbc.ChiTietTCs.Add(p);
                int kt = dbc.SaveChanges();
                if (kt > 0)
                {
                    var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, p.UserId, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "InsertThuChi-" + p.NgayTC, "");
                    Session["ThongBaoThuChiUser"] = "Insert thành công File Thu Chi Ngày: " + p.NgayTC;
                    
                    return RedirectToAction("ListThuChiTeK");
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                ModelState.AddModelError("", "Thêm mới Thất Bại !!!!!!!!!!. Có lỗi hệ thống");
                return View("InsertThuChi");
            }
            return RedirectToAction("Index");
        }
        public ActionResult LoadKyXuatNhap()
        {
            var Kyxuatnhap = dbc.KyXuatNhaps.Where(kh => kh.AdminXacNhan == true && kh.Id > 1
                 && dbc.ChiTietTCs.FirstOrDefault(nn => nn.IdKyxuatnhap == kh.Id) == null)
                     .OrderByDescending(kh => kh.NgayAuto).Select(kh => new
                     {
                         Id = kh.Id,
                         TenKy = kh.TenKy
                     });
            return Json(Kyxuatnhap, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getTongTien(int Id)
        {
            var tong = new XuatNhapData().getTongTienAuto(Id);
            return Json(tong, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DayThuChiTek(string Id, int key)
        {
            var iiii = new Guid(Id.ToString());
            var ThuChi = dbc.ChiTietTCs.Find(iiii);
            if (ThuChi != null)
            {
                if (key == 1)
                {
                    if (ThuChi.YeuCauDay == true)
                    {
                        ThuChi.YeuCauDay = false;
                    }
                    else ThuChi.YeuCauDay = true;
                }
                else if (key == 2)
                {
                    if (ThuChi.AdminXacNhan == true)
                    {
                        ThuChi.AdminXacNhan = false;
                    }
                    else
                    {
                        ThuChi.AdminXacNhan = true;
                    }
                }

                dbc.Entry(ThuChi).State = EntityState.Modified;
                dbc.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteThuChi(string Id)
        {
            var iiii = new Guid(Id);
            var model = dbc.ChiTietTCs.Find(iiii);
            if (model != null)
            {
                try
                {
                    var ngay = model.NgayTC;
                    dbc.ChiTietTCs.Remove(model);
                    dbc.SaveChanges();
                    Session["ThongBaoThuChiUser"] = "Delete thành công file thu chi ngày: " + ngay.ToString("{dd/MM/yyyy}");
                }
                catch (Exception ex)
                {
                    var loi = ex.Message;
                }
            }
            return RedirectToAction("ListThuChiUser", "Home");
        }
        private string saveFile_imgthuchi(HttpPostedFileBase File)
        {
            if (File.ContentLength > 0)
            {
                var ten = File.FileName;
                string[] str = ten.Split('.');

                var ext = str[str.Count() - 1].ToLower();
                if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "xls" || ext == "pdf" || ext == "xlsx"
                    || ext == "doc" || ext == "docx")
                {
                    var sub = XString.MakeAotuName();
                    ten = str[str.Count() - 2] + sub + "." + ext;
                    //p.Filesave1 = ten;
                    //Thu nho hinh anh qua to
                    WebImage img = new WebImage(File.InputStream);
                    if (img.Width > 400)
                        img.Resize(400, 450);
                    img.Save(Server.MapPath("~/Areas/Admin/Content/imgthuchi/" + ten));
                }
                else ten = "";
                return ten;
            }
            return "";
        }
    }
}