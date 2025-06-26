using DoChoiXeMay.Models;
using DoChoiXeMay.Utils;
using MaHoa_GiaiMa_TaiKhoan;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Controllers
{
    public class AccountWebController : Controller
    {
        // GET: UserWeb
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _ForgotPass()
        {
            return View("_ForgotPass");
        }
        [HttpPost]
        public ActionResult _ForgotPass(string UserName)
        {
            if (UserName != null) {
                var user_mem = dbc.UserTeks.FirstOrDefault(kh=>kh.UserName== UserName);
                if (user_mem != null) {
                    TaiKhoanInfo tk = new TaiKhoanInfo();
                    string pas = tk.DeCryptDotNetNukePassword(user_mem.Password.ToString(), "A872EDF100E1BC806C0E37F1B3FF9EA279F2F8FD378103CB", user_mem.PasswordSalt.ToString());
                    var kq = Mailer.Send(user_mem.EmailConnection, "Quên password được gửi từ tek-lightning", "password của bạn là: " + pas);
                    if (kq)
                    {
                        ModelState.AddModelError("", "Đã gửi mật khẩu đến email " + user_mem.EmailConnection);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Có lỗi !!! Không thể gửi mật khẩu đến email " + user_mem.EmailConnection + " !!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi !!! User này không tồn tại !!!");
                }
                
            }
            return View("_ForgotPass");
        }
        public ActionResult _ChangeInFo(int Id)
        {
            var model = dbc.Ser_ChiNhanh.Find(Id);
            return View(model);
        }
    }
}