using DoChoiXeMay.Models;
using MaHoa_GiaiMa_TaiKhoan;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static System.Net.Mime.MediaTypeNames;

namespace DoChoiXeMay.Controllers
{
    public class HomeController : Controller
    {
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            var cookie = Request.Cookies["user_log_new"];
            if (cookie != null)
            {
                ViewBag.UserName = cookie.Values["User"];
                ViewBag.Password = cookie.Values["Pw"];
                ViewBag.reme = "kh";
            }
            else
            {
                ViewBag.UserName = "";
                ViewBag.Password = "";
                ViewBag.reme = "";
            }
            return View("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(String UserName, String Password, System.Boolean? remember)
        {
            var tk = new UserTek();
            var requestUri = Session["requestUri"] as string;
            TaiKhoanInfo tk_check = new TaiKhoanInfo();
            //tài khoản không phân biệt hoa thường.
            var user = dbc.UserTeks.Where(p => p.UserName.ToLower() == UserName.ToLower()).SingleOrDefault();
            if (user != null)
            {
                var time_locked = user.LastLokedChangedate;
                var check_time = DateTime.Now - time_locked;

                // Kiểm tra xem thời giạn bị khóa trên 5 chưa ? Nếu trên 5p reset lại thành false
                if (check_time.Minutes > 5 && user.Islocked == true)
                {
                    user.Islocked = false;
                    user.CountFailedPassword = 0;
                    dbc.SaveChanges();
                }
                // Sau khi kiểm tra tình trạng khóa tài khoản thì kiểm tra đăng nhập
                if (user.Islocked == true)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa.Vui lòng đăng nhập sau " + (5 - check_time.Minutes) + " phút " + (60 - check_time.Seconds) + " giây");
                }
                else
                {
                    string check_pass = tk_check.EnCryptDotNetNukePassword(Password, "", user.PasswordSalt);//pass ma hoa
                    if (user.Password == check_pass)
                    {
                        int UserFirt = user.Id;
                        var quyenAd = dbc.UserTeks.Where(p => p.UserName == UserName && p.IdLoai == 1).ToList();
                        var quyensub = dbc.UserTeks.Where(p => p.UserName == UserName && p.IdLoai == 2).ToList();
                        var quyenguest = dbc.UserTeks.Where(p => p.UserName == UserName && p.IdLoai == 2).ToList();
                        if (UserFirt == -1 || UserFirt == 0 || quyenAd.Count == 0 && quyensub.Count == 0 && quyenguest.Count == 0)
                        {
                            ModelState.AddModelError("", "Chào bạn, tài khoản của bạn không sử dụng được trên web của chúng tôi. Liên Hệ trung tâm để được hổ trợ, hoặc tạo tài khoản mới để đăng nhập web !!");
                        }
                        else
                        {
                            // đăng nhập thành công set lại số lần nhập sai = 0
                            if (user.CountFailedPassword > 0)
                            {
                                user.CountFailedPassword = 0;
                                dbc.Entry(user).State = EntityState.Modified;
                                dbc.SaveChanges();
                            }


                            string[] user_log = new string[2];
                            user_log[0] = UserName;
                            user_log[1] = Password;
                            Session["UserName"] = UserName;
                            Session["UserId"] = UserFirt;

                            var cookie = new HttpCookie("user_log_new");
                            if (remember != null)
                            {
                                if (remember == true)
                                {
                                    cookie.Values["User"] = UserName;
                                    cookie.Values["Pw"] = Password;
                                    cookie.Expires = DateTime.Now.AddMonths(1);
                                }
                                else
                                {
                                    cookie.Expires = DateTime.Now;
                                }
                            }
                            else
                            {
                                cookie.Expires = DateTime.Now.AddDays(-1);
                            }
                            Response.Cookies.Add(cookie);
                            if (user.IdLoai == 1 || user.IdLoai == 2)
                            {
                                Session["quyen"] = user.LoaiUserTek.LoaiUser;
                                Session["avatar"] = user.Avatar;
                                var uID = UserFirt;
                                var model_uid = dbc.UserTeks.Find(UserFirt);
                                bool nhatky = Areas.Admin.Data.XuatNhapData.InsertNhatKy_Admin(dbc, UserFirt, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "LoginWeb", "");
                                //tro lai trang truoc do 
                                if (requestUri != null)
                                {
                                    return Redirect(requestUri);
                                }
                                return RedirectToAction("ListThuChiTeK", "ThuChi", new { area = "Admin" });
                            }
                            else
                            {
                                Session["quyen"] = user.LoaiUserTek.LoaiUser;
                                var uID = UserFirt;
                                var model_uid = dbc.UserTeks.Find(uID);
                                bool nhatky = Areas.Admin.Data.XuatNhapData.InsertNhatKy_Admin(dbc, UserFirt, Session["quyen"].ToString()
                        , Session["UserName"].ToString(), "LoginWeb", "");
                            }
                            Session.Remove("Thongbaodangky");
                            //tro lai trang truoc do 
                            if (requestUri != null)
                            {
                                return Redirect(requestUri);
                            }
                            else { return RedirectToAction("Index", "Home"); }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu sai vui lòng nhập lại");
                        //đếm số lần nhập sai
                        user.CountFailedPassword += 1;
                        if (user.CountFailedPassword == 5)
                        {
                            //Sai 5 lần tài khoản bị khóa
                            user.Islocked = true;
                            user.lastPasswordChangedate = DateTime.Now;
                            dbc.Entry(user).State = EntityState.Modified;
                            dbc.SaveChanges();
                            ModelState.AddModelError("", "Nhập sai password liên tiếp 5 lần !!! Tài khoản của bạn bị khóa !!!");
                        }
                        else
                        {
                            dbc.Entry(user).State = EntityState.Modified;
                            dbc.SaveChanges();
                            ModelState.AddModelError("", "Nhập sai password lần: " + user.CountFailedPassword);
                        }
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Sai tên đăng nhập");
            }
            return View();
        }
    }
}