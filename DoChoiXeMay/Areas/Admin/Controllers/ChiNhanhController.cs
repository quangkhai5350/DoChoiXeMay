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
    public class ChiNhanhController : Controller
    {
        // GET: Admin/ChiNhanh
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            Session["requestUri"] = "/Admin/ChiNhanh/Index";
            return View();
        }
        public ActionResult InsertAutoSNChiNhanh()
        {
            try
            {
                var userid = int.Parse(Session["UserId"].ToString());
                Ser_XuatSN_CN model = new Ser_XuatSN_CN();
                model.UserId = userid;
                model.SoLuong = 0;
                model.DaAdd = 0;
                model.NgayXuat = DateTime.Now;
                model.IdKyxuat = 1;
                model.IdChiNhanh = 1;
                model.Ghichu = "Mới tạo auto";
                dbc.Ser_XuatSN_CN.Add(model);
                dbc.SaveChanges();
                Session["ThongBaoXuatSN_ChiNhanh"] = "Auto xuất S/N cho ChiNhanh thành công, cần update - Số Lượng - Kỳ Xuất và ChiNhanh.";
                var nhatky = Data.XuatNhapData.InsertNhatKy_Admin(dbc, userid, Session["quyen"].ToString()
                            , Session["UserName"].ToString(), "Xuất 0 serial cho chi nhánh - " + model.IdChiNhanh + "-" + DateTime.Now.ToString(), "");
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
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetListSNchinhanh()
        {
            var model = dbc.Ser_XuatSN_CN.OrderByDescending(kh => kh.Id)
                .ThenByDescending(kh => kh.IdChiNhanh)
                .ThenByDescending(kh=>kh.SoLuong).ToList();
            ViewBag.GetListSNchinhanh = model;
            return PartialView(model);
        }
        public ActionResult DeleteSNChiNhanh( int Id) {
            var modelchitiet = dbc.Ser_Chitiet_XuatSN_CN.FirstOrDefault(kh => kh.IdSN_CN==Id);
            if(modelchitiet == null)
            {
                var model = dbc.Ser_XuatSN_CN.Find(Id);
                dbc.Ser_XuatSN_CN.Remove(model);
                dbc.SaveChanges();
                Session["ThongBaoXuatSN_ChiNhanh"] = "Delete đợt S/N cho ChiNhanh thành công.";
            }
            else
            {
                Session["ThongBaoXuatSN_ChiNhanh"] = "Đã có S/N add chi nhánh, Delete đợt S/N cho ChiNhanh thất bại !!!";
            }
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateSNchoCN(int Id)
        {
            var model = dbc.Ser_XuatSN_CN.Find(Id);
            ViewBag.IdKyxuat = new SelectList(dbc.KyXuatNhaps.Where(kh => kh.AdminXNPUSH == true && kh.UPush==true), "Id", "TenKy", model.IdKyxuat);
            ViewBag.IdChiNhanh = new SelectList(dbc.Ser_ChiNhanh.Where(kh => kh.Sudung == true), "Id", "TenChiNhanh", model.IdChiNhanh);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateSNchoCN(Ser_XuatSN_CN model)
        {
            try
            {
                model.NgayXuat = DateTime.Now;
                if(model.Ghichu=="" || model.Ghichu == "Mới tạo auto")
                {
                    model.Ghichu = "Đã update ngày: "+DateTime.Now.ToShortDateString();
                }
                dbc.Entry(model).State = EntityState.Modified;
                dbc.SaveChanges();
                if(model.SoLuong>0 && model.IdKyxuat>1 && model.IdChiNhanh > 1)
                {
                    Session["ThongBaoXuatSN_ChiNhanh"] = "Update thành công, bạn đã có thể add S/N cho ChiNhanh.";
                }
                else
                {
                    Session["ThongBaoXuatSN_ChiNhanh"] = "Update thành công.";
                }
                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                string message = ex.Message;
                ViewBag.IdKyxuat = new SelectList(dbc.KyXuatNhaps.Where(kh => kh.AdminXNPUSH == true && kh.UYeuCauThuHoi == false), "Id", "TenKy", model.IdKyxuat);
                ViewBag.IdChiNhanh = new SelectList(dbc.Ser_ChiNhanh.Where(kh => kh.Sudung == true), "Id", "TenChiNhanh", model.IdChiNhanh);
                ModelState.AddModelError("", "Update Thất Bại !!!!" + message);
                return View(model);
            }
        }
        public ActionResult AddSN_ChiNhanh(int Id)
        {
            var model = dbc.Ser_Chitiet_XuatSN_CN.Where(kh => kh.IdSN_CN == Id).ToList();
            ViewBag.GetListChitietSN_CN=model;
            Session["TenChiNhanh"] = new Data.ChiNhanhData().GetChiNhanhByIdXuat(Id).TenChiNhanh;
            Session["KyXuatNhap"] = new Data.ChiNhanhData().GetKyByIdXuat(Id).TenKy;
            Session["IdSN_CN"] = Id;
            Session["DaAdd"] = model.Count();
            Session["SoLuong"] = dbc.Ser_XuatSN_CN.Find(Id).SoLuong;
            return View(model);
        }
        public ActionResult TimSNLoHang(string Serial)
        {
            var modelct = dbc.Ser_Chitiet_XuatSN_CN.FirstOrDefault(kh => kh.Serial == Serial);
            if (modelct != null)
            {
                Session["ThongBaoXuatSN_ChiNhanh"] = "Số serial: " + Serial + " đã nằm trong lô hàng của chi nhánh "
                    + modelct.Ser_XuatSN_CN.Ser_ChiNhanh.TenChiNhanh + " có Id=" + modelct.IdSN_CN + ".";
            }
            else
            {
                Session["ThongBaoXuatSN_ChiNhanh"] = "Số serial: " + Serial + " vẫn CHƯA add lô hàng nào.";
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddSNCNtoChiTiet(string Serial)
        {
            var Id = int.Parse(Session["IdSN_CN"].ToString());
            var modelctxuat = dbc.Ser_Chitiet_XuatSN_CN.Where(kh => kh.IdSN_CN == Id).ToList();
            var soluong = dbc.Ser_XuatSN_CN.Find(Id).SoLuong;
            if (Session["IdSN_CN"] != null && Serial.Trim() !="")
            {
                try
                {
                    var modelct = dbc.Ser_Chitiet_XuatSN_CN.FirstOrDefault(kh => kh.Serial == Serial);
                    if (modelct != null)
                    {
                        Session["ThongBaotaolohang"] = "Số serial: " + Serial + " đã nằm trong lô hàng của chi nhánh "
                            + modelct.Ser_XuatSN_CN.Ser_ChiNhanh.TenChiNhanh + " có Id=" + modelct.IdSN_CN + ", không thể add !!!";
                    }else if (modelctxuat.Count()== soluong)
                    {
                        Session["ThongBaotaolohang"] = "Đã add đủ S/N cho lô hàng này, không thể add thêm !!!";
                    }
                    else
                    {
                        Ser_Chitiet_XuatSN_CN model = new Ser_Chitiet_XuatSN_CN();
                        model.Id = Guid.NewGuid();
                        model.Serial = Serial;
                        model.IdSN_CN = int.Parse(Session["IdSN_CN"].ToString());
                        model.NgayXuat = DateTime.Now;
                        dbc.Ser_Chitiet_XuatSN_CN.Add(model);
                        dbc.SaveChanges();
                        modelctxuat = dbc.Ser_Chitiet_XuatSN_CN.Where(kh => kh.IdSN_CN == Id).ToList();
                        Session["DaAdd"] = modelctxuat.Count();
                        Session["ThongBaotaolohang"] = "Thành công add S/N: " + Serial + " cho Chi nhánh";
                        //Update DaAdd cho Ser_XuatSN_CN
                        var modelXuat = dbc.Ser_XuatSN_CN.Find(int.Parse(Session["IdSN_CN"].ToString()));
                        modelXuat.DaAdd = modelctxuat.Count();
                        dbc.Entry(modelXuat).State = EntityState.Modified;
                        dbc.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    string message = ex.Message;
                    Id = int.Parse(Session["IdSN_CN"].ToString());
                    //modelctxuat = dbc.Ser_Chitiet_XuatSN_CN.Where(kh => kh.IdSN_CN == Id).ToList();
                    //ViewBag.GetListChitietSN_CN = modelctxuat;
                    Session["TenChiNhanh"] = new Data.ChiNhanhData().GetChiNhanhByIdXuat(Id).TenChiNhanh;
                    Session["KyXuatNhap"] = new Data.ChiNhanhData().GetKyByIdXuat(Id).TenKy;
                    return View("AddSN_ChiNhanh");
                }
                
            }
            
            //ViewBag.GetListChitietSN_CN = modelctxuat;
            Session["TenChiNhanh"] = new Data.ChiNhanhData().GetChiNhanhByIdXuat(Id).TenChiNhanh;
            Session["KyXuatNhap"] = new Data.ChiNhanhData().GetKyByIdXuat(Id).TenKy;
            return View("AddSN_ChiNhanh");
        }
    }
}