using DoChoiXeMay.Models;
using MaHoa_GiaiMa_TaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class UserData
    {
        Model1 _context;
        public UserData()
        {
            _context = new Model1();
        }
        public UserTek GetUserbyID(int IDUser)
        {
            var User = _context.UserTeks.Find(IDUser);
            return User;
        }
        public string PassbyPass(int UserId) {
            TaiKhoanInfo tk = new TaiKhoanInfo();
            var UserChinhanh = _context.UserTeks.Find(UserId);
            string check_pass = tk.DeCryptDotNetNukePassword(UserChinhanh.Password, "A872EDF100E1BC806C0E37F1B3FF9EA279F2F8FD378103CB", UserChinhanh.PasswordSalt);//pass ma hoa
            return check_pass;
        }
    }
}