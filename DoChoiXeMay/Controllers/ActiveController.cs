using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoChoiXeMay.Controllers
{
    public class ActiveController : Controller
    {
        // GET: Active
        Model1 dbc = new Model1();
        public ActionResult Index()
        {
            //(bool)Session["Pay"] = true
            if (Session["ND"]==null && Session["NPP"] == null)
            {
                ViewBag.ND = "ND";
                ViewBag.NPP = "NONPP";
            }
            else
            {
                ViewBag.ND = Session["ND"].ToString();
                ViewBag.NPP = Session["NPP"].ToString();
            }
            ViewBag.IdChiNhanh = new SelectList(dbc.Ser_ChiNhanh
                .OrderByDescending(kh=>kh.IdLevel)
                .ThenByDescending(kh=>kh.Id)
                .Where(kh => kh.Sudung == true), "Id", "TenChiNhanh");
            return View();
        }
        public ActionResult IndexCheckBHND(string SerialSP)
        {
            try
            {
                Session["thongtin1"] = ""; Session["thongtin2"] = ""; 
                Session["thongtin3"] = ""; Session["thongtin4"] = "";
                Session["ThongbaoActive"] = "";
                var serialSP = dbc.Ser_sp.FirstOrDefault(kh => kh.SerialSP == SerialSP && kh.DaIn == true && kh.Sudung == true);
                if (serialSP != null) {
                    var kt = dbc.Ser_kichhoat.FirstOrDefault(kh => kh.IDSer_sp == serialSP.Id);
                    if (kt != null && kt.IdChiNhanh==1)
                    {
                        Session["thongtin1"] = "S/N:"+ SerialSP + ", được kích hoạt bởi: " + kt.TenKhachHang+", sdt:"+kt.SDT;
                        Session["thongtin2"] = "_Sản phẩm đã kích hoạt ngày "+kt.NgayKichHoat;
                        Session["thongtin3"] = "_Sản phẩm có ngày hết hạn bảo hành: " + kt.Ghichu;
                        Session["thongtin4"] = "_Sản phẩm có tình trạng: " + kt.Ser_trangthai.Name +".";
                        return Json("22", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Session["ThongbaoActive"] = "Không thể kiểm tra: S/N "+ SerialSP + " đã kích hoạt, nhưng không có thông tin, liên hệ TeK để biết thêm chi tiết.";
                        return Json("22", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Session["ThongbaoActive"] = "Không thể kiểm tra: S/N "+ SerialSP + " không tồn tại, hoặc vẫn chưa kích hoạt!!!";
                    return Json("22", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                Session["ThongbaoActive"] = "Có Lỗi hệ thống khi kiểm tra BH ND !!!";
                return Json("22", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult KichHoatBaoHanh(bool ND,bool NPP, string Tenkh, string sBOX, string sSP, int IdChiNhanh, string gmail,string sdt) {
            try
            {
                Session["thongtin1"] = ""; Session["thongtin2"] = ""; 
                Session["thongtin3"] = ""; Session["thongtin4"] = ""; Session["ThongbaoActive"] = "";
                var serial = dbc.Ser_sp.FirstOrDefault(kh => kh.SerialSP == sSP && kh.DaIn == true && kh.Sudung == false);
                var serialb = dbc.Ser_box.FirstOrDefault(kh => kh.Serial == sBOX && kh.DaIn == true && kh.Sudung == false);
                if (ND)
                {
                    if (serial != null)
                    {
                        if (serialb != null)
                        {
                            var Ac = new Areas.Admin.Data.ActiveData().InsertKichHoatBH(true,
                                serialb.Id.ToString(), serial.Id.ToString(), IdChiNhanh, gmail, Tenkh, sdt);
                            if (Ac)
                            {
                                var kqBox = new Areas.Admin.Data.SerialData().UpdateSer_box(serialb.Id.ToString(),
                                    true, true, serialb.NgayUpdate, "Active");
                                var kqSP = new Areas.Admin.Data.SerialData().UpdateSer_SP(serial.Id.ToString(),
                                    true, true, serial.NgayUpdate, serial.HangTangKhongBan);
                                return Json("yes", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("11", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("22", JsonRequestBehavior.AllowGet);
                    }
                }else if (NPP)
                {
                    if (serial != null)
                    {
                        if (serialb != null)
                        {
                            var chinhanh = dbc.Ser_ChiNhanh.Find(IdChiNhanh);
                            var Ac = new Areas.Admin.Data.ActiveData().InsertKichHoatBH(true,
                                serialb.Id.ToString(), serial.Id.ToString(), IdChiNhanh, gmail, "", sdt);
                            if (Ac)
                            {
                                var kqBox = new Areas.Admin.Data.SerialData().UpdateSer_box(serialb.Id.ToString(),
                                    true, true, serialb.NgayUpdate, "Active");
                                var kqSP = new Areas.Admin.Data.SerialData().UpdateSer_SP(serial.Id.ToString(),
                                    true, true, serial.NgayUpdate, serial.HangTangKhongBan);
                                return Json("yes", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("11", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("22", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("33", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return Json("33", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChitietNPP(int NPP=0)
        {
            try
            {
                var daily = dbc.Ser_ChiNhanh.Find(NPP);
                var kq = daily.SDT + "/" + daily.Gmail;
                return Json( kq, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            
        }
    }
}