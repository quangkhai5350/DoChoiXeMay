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

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            Session["UserName"] = "Admin";
            Session["UserId"] = "1";
            Session["quyen"] = "Admin";
            return View();
        }
        [Protect]
        [HttpGet]
        public ActionResult InsertThuChi()
        {
            var model = new ChiTietTC();
            ViewBag.IdMa = new SelectList(dbc.MaTCs.Where(kh => kh.SuDung == true), "Id", "TenMa");
            
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

                var file = Request.Files["Filesave1"];
                if (file.ContentLength > 0)
                {
                    var ten = file.FileName;
                    string[] str = ten.Split('.');

                    //var ext = ten.Substring(ten.LastIndexOf('.'));
                    var ext = str[str.Count()-1].ToLower();
                    //var ext = ten.Substring(ten.LastIndexOf('.')).ToLower();
                    if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "xls"|| ext == "pdf"||ext=="xlsx"
                        ||ext =="doc"||ext =="docx")
                    {
                        var sub = XString.MakeAotuName();
                        ten = str[str.Count()-2] + sub + "."+ext;
                        p.Filesave1 = ten;
                        //Thu nho hinh anh qua to
                        WebImage img = new WebImage(file.InputStream);
                        if (img.Width > 400)
                            img.Resize(400, 450);
                        img.Save(Server.MapPath("~/Areas/Admin/Content/imgthuchi/" + ten));
                        
                    }
                    else p.Filesave1 = "";
                }
                //else dn.Logo = "logo/TNThumbnail.jpg";
                //model.Logo = dn.Logo;
                ////p.Filesave1 = "";
                ////p.Filesave2 = "";
                ////p.Filesave3 = "";
                ////p.NgayTC = TC.NgayTC;
                p.NgayAuto = DateTime.Now;
                p.UserId = int.Parse(Session["UserId"].ToString());
                p.AdminXacNhan = false;
                dbc.ChiTietTCs.Add(p);
                int kt = dbc.SaveChanges();
                if(kt > 0)
                {
                    var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, p.UserId, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "InsertThuChi-" + p.NgayTC, "");
                    Session["ThongBao_InsertTC"] = "Insert thành công File Thu Chi Ngày: " + p.NgayTC;
                    return RedirectToAction("Index");
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
           var Kyxuatnhap = dbc.KyXuatNhaps.Where(kh => kh.AdminXacNhan == true && kh.Id>1
                && dbc.ChiTietTCs.FirstOrDefault(nn => nn.IdKyxuatnhap == kh.Id) == null)
                    .OrderByDescending(kh => kh.NgayAuto).Select(kh => new
                    {
                        Id=kh.Id,
                        TenKy = kh.TenKy
                    });
            return Json(Kyxuatnhap, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getTongTien(int Id)
        {
            var tong = new XuatNhapData().getTongTienAuto(Id);
            return Json(tong, JsonRequestBehavior.AllowGet);
        }
    }
}