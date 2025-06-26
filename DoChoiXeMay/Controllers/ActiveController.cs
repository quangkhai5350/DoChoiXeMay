using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoChoiXeMay.Areas.Admin.Data;

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
            ViewBag.ChiNhanh = dbc.Ser_ChiNhanh.Where(kh => kh.Id > 1)
                                .OrderByDescending(kh => kh.IdLevel)
                                .ThenByDescending(kh => kh.Id)
                                .ToList();
            ViewBag.KhuVuc = new SelectList(dbc.KhuVucs.ToList(), "Id", "TenKhuvuc");
            return View();
        }
        [ProtectKH]
        public ActionResult IndexDL()
        {
            var chinhanh = dbc.Ser_ChiNhanh.Find(int.Parse(Session["idchinhanhAt"].ToString()));
            Session["thongtinDL"] = chinhanh.TenChiNhanh;
            if (Session["ND"] == null && Session["NPP"] == null)
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
                .OrderByDescending(kh => kh.IdLevel)
                .ThenByDescending(kh => kh.Id)
                .Where(kh => kh.Sudung == true), "Id", "TenChiNhanh",chinhanh.Id);
            ViewBag.KhuVuc = new SelectList(dbc.KhuVucs.ToList(), "Id", "TenKhuvuc");
            ViewBag.TotalSerialDaKH = dbc.Ser_kichhoat.Where(kh => kh.IdChiNhanh == chinhanh.Id).Count();
            return View(chinhanh);
        }
        [ProtectKH]
        public ActionResult GetListKHBHbyChiNhanh(string tu, string den, int PageNo = 0, int PageSize = 0, int IdCN = 0, string KeywordsTTT = "")
        {
            var model = new ActiveData().getSNACTek(PageNo, PageSize, IdCN, KeywordsTTT,tu,den);

            var chinhanh = dbc.Ser_ChiNhanh.Find(int.Parse(Session["idchinhanhAt"].ToString()));
            //model = model.Where(kh => kh.IdChiNhanh == chinhanh.Id).ToList();
            ViewBag.ListSerialKH = model;
            return PartialView(chinhanh);
        }
        [ProtectKH]
        public ActionResult GetPageCountActive(string tu, string den, int PageSize = 0, int IdCN = 0, string KeywordsTTT = "")
        {
            var chinhanh = dbc.Ser_ChiNhanh.Find(int.Parse(Session["idchinhanhAt"].ToString()));
            var num =  ActiveData.ChiTietkichhoatDBTEK(dbc, IdCN, KeywordsTTT, tu, den).Count();
            var pageCount = Math.Ceiling(1.0 * num / PageSize);
            return Json(pageCount, JsonRequestBehavior.AllowGet);
        }
        [ProtectKH]
        public ActionResult GetTongSanPhamAC(string tu, string den, int IdCN, string KeywordsTTT = "")
        {
            var chinhanh = dbc.Ser_ChiNhanh.Find(int.Parse(Session["idchinhanhAt"].ToString()));
            var num = ActiveData.ChiTietkichhoatDBTEK(dbc, IdCN, KeywordsTTT, tu, den).Count();
            return Json(num, JsonRequestBehavior.AllowGet);
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
                    if (kt != null)
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
        public ActionResult KichHoatBaoHanh(bool ND,bool NPP, string Tenkh = "", string sBOX="", string sSP="", string ChiNhanh="", string gmail = "",string sdt = "",string khuvuc="") {
            try
            {
                Session["thongtin1"] = ""; Session["thongtin2"] = ""; 
                Session["thongtin3"] = ""; Session["thongtin4"] = ""; Session["ThongbaoActive"] = "";
                var serial = dbc.Ser_sp.FirstOrDefault(kh => kh.SerialSP == sSP && kh.DaIn == true && kh.Sudung == false);
                var serialb = dbc.Ser_box.FirstOrDefault(kh => kh.Serial == sBOX && kh.DaIn == true && kh.Sudung == false);
                var tenchinhanh = dbc.Ser_ChiNhanh.FirstOrDefault(x => x.TenChiNhanh == ChiNhanh.Trim());
                if (ND)
                {
                        if (serial != null)
                        {
                            if (serialb != null)
                            {
                                var Ac = new ActiveData().InsertKichHoatBH(
                                    serialb.Id.ToString(), serial.Id.ToString(), 1, gmail, Tenkh, sdt, khuvuc);
                                if (Ac > -1)
                                {
                                    var kqBox = new SerialData().UpdateSer_box(serialb.Id.ToString(),
                                        true, true, serialb.NgayUpdate, "Active");
                                    var kqSP = new SerialData().UpdateSer_SP(serial.Id.ToString(),
                                        true, true, serial.NgayUpdate, serial.HangTangKhongBan);
                                    return Json(Ac.ToString(), JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json("111", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("222", JsonRequestBehavior.AllowGet);
                        }
                    
                }else if (NPP)
                {
                    if (tenchinhanh != null)
                    {
                        if (serial != null)
                        {
                            if (serialb != null)
                            {
                                
                                var Ac = new ActiveData().InsertKichHoatBH(
                                    serialb.Id.ToString(), serial.Id.ToString(), tenchinhanh.Id, gmail, Tenkh, sdt, khuvuc);
                                if (Ac > -1)
                                {
                                    var kqBox = new SerialData().UpdateSer_box(serialb.Id.ToString(),
                                        true, true, serialb.NgayUpdate, "Active");
                                    var kqSP = new SerialData().UpdateSer_SP(serial.Id.ToString(),
                                        true, true, serial.NgayUpdate, serial.HangTangKhongBan);
                                    return Json(Ac.ToString(), JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json("111", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("222", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("444", JsonRequestBehavior.AllowGet);
                    }

                }
                return Json("333", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return Json("333", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChitietNPP(string NPP="")
        {
            try
            {
                var daily = dbc.Ser_ChiNhanh.FirstOrDefault(kh=>kh.TenChiNhanh==NPP);
                if (daily != null) {
                    var kq = daily.SDT + "/" + daily.Gmail;
                    return Json(kq, JsonRequestBehavior.AllowGet);
                }
                return Json("No", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                string loi = exc.ToString();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            
        }
    }
}