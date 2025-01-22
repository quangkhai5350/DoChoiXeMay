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
using MaHoa_GiaiMa_TaiKhoan;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;
using System.Reflection;

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
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var model = dbc.UserTeks.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditUser(UserTek model, string PW, string PWM, string PWMM)
        {
            TaiKhoanInfo tk = new TaiKhoanInfo();
            string check_pass = tk.DeCryptDotNetNukePassword(model.Password, "A872EDF100E1BC806C0E37F1B3FF9EA279F2F8FD378103CB", model.PasswordSalt);//pass ma hoa
            if (PW == check_pass)
            {
                if(PWM == PWMM)
                {
                    var checkname = dbc.UserTeks.Where(kh => kh.UserName == model.UserName && kh.Id!=model.Id);
                    if (checkname.Count() == 0)
                    {
                        string PasswordSalt = Convert.ToBase64String(tk.GenerateSalt()); //tạo chuổi salt ngẫu nhiên
                        string cipherPass = tk.EnCryptDotNetNukePassword(PWM, "", PasswordSalt);

                        model.Password = cipherPass;
                        model.PasswordSalt = PasswordSalt;
                        model.lastPasswordChangedate = DateTime.Now;
                        dbc.Entry(model).State = EntityState.Modified;
                        dbc.SaveChanges();
                        var uid = int.Parse(Session["UserId"].ToString());
                        var sms = Session["UserName"].ToString().ToUpper() + " đã tự update user của mình Thành Công." + model.Createdate.ToString("{dd/MM/yyyy}");
                        var Msg = Data.XuatNhapData.InsertMsgAotu(dbc, uid, sms, false, false, false, false, false);
                        //Insert Nhật Ký
                        var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, uid, Session["quyen"].ToString()
                                , Session["UserName"].ToString(), "Update user của mình Thành Công ", "");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update Thất Bại !!!!!!!!!!. Tên đăng nhập bị trùng lặp");
                        return View();
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Update Thất Bại !!!!!!!!!!. PassWord Xác nhận không giống");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Update Thất Bại !!!!!!!!!!. PassWord củ không đúng");
                return View();
            }
            
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("ListThuChiTeK","ThuChi");
        }
        public ActionResult ListHangHoaTek()
        {
            Session["requestUri"] = "/Admin/Home/ListHangHoaTek";
            return View();
        }
        public ActionResult GetListHHTEK()
        {
            double tong = 0;
            var model = dbc.HangHoas
                .OrderBy(h => h.Id)
                .ToList();
            for (int i = 0; i < model.Count(); i++)
            {
                model[i].GhiChu = (i + 1).ToString();
                tong=tong+ model[i].SoLuong * model[i].GiaNhap;
            }
            ViewBag.HHTEK=model.OrderByDescending(h => h.Id).ToList();
            ViewBag.Tongsp = model.Sum(kh => kh.SoLuong);
            ViewBag.TongLoai = model.Count();
            ViewBag.TongTien = tong;
            return PartialView();
        }
    }
}