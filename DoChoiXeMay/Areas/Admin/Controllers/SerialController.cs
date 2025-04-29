using DoChoiXeMay.Areas.Admin.Data;
using DoChoiXeMay.Filters;
using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DoChoiXeMay.Areas.Admin.Controllers
{
    [Protect]
    public class SerialController : Controller
    {
        // GET: Admin/Serial
        Model1 dbc = new Model1();
        string DBname = ConfigurationManager.AppSettings["DBname"];
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListSerialChuaIn()
        {
            ViewBag.TotalSerialSPchuaIn = dbc.Ser_sp.Where(kh=>kh.DaIn==false).Count();
            ViewBag.TotalSerialBoXchuaIn = dbc.Ser_box.Where(kh => kh.DaIn == false).Count();
            return View();
        }
        public ActionResult AddNewSerial()
        {
            ViewBag.IDMF = new SelectList(dbc.Manufacturers.Where(kh => kh.Sudung == true), "Id", "Name",5);
            ViewBag.IdColor = new SelectList(dbc.Colors.ToList(), "Id", "TenColor");
            ViewBag.IdSize = new SelectList(dbc.Sizes.ToList(), "Id", "TenSize");
            ViewBag.Idver = new SelectList(dbc.Versions.ToList(), "Id", "VerName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewSerial(Ser_sp SerSP,int SoSerialN, string soloName, string soloSTT)
        {
            ViewBag.IDMF = new SelectList(dbc.Manufacturers.Where(kh => kh.Sudung == true), "Id", "Name", 5);
            ViewBag.IdColor = new SelectList(dbc.Colors.ToList(), "Id", "TenColor");
            ViewBag.IdSize = new SelectList(dbc.Sizes.ToList(), "Id", "TenSize");
            ViewBag.Idver = new SelectList(dbc.Versions.ToList(), "Id", "VerName");
            string solosp = soloName + soloSTT;
            string solobox = "BXX" + soloSTT;
            try
            {
                //tạo S/N SP
                for (int i = 0; i < SoSerialN; i++)
                {
                    SerSP.LoSanXuat = solosp;
                    string SN = Utils.XString.MakeAotuSN(10) + SerSP.LoSanXuat;
                    var kt = dbc.Ser_sp.Where(kh => kh.SerialSP.Contains(SN)).ToList();
                    if (kt.Count() > 0)
                    {
                        //Nếu S/N bị trùng, random 50 lần
                        for (int j = 0; j < 50; j++) {
                            SN= Utils.XString.MakeAotuSN(10) + SerSP.LoSanXuat;
                            var ktt = dbc.Ser_sp.Where(kh => kh.SerialSP.Contains(SN)).ToList();
                            if (ktt.Count() == 0)
                            {
                                SerSP.SerialSP = SN;
                                break;
                            }
                        }
                    }
                    else
                    {
                        SerSP.SerialSP = SN;
                    }
                    SerSP.Stt = (i + 1).ToString();
                    
                    var kq = new Data.SerialData().InsertSer_sp(SerSP);
                    if (kq==false)
                    {
                        Session["ThongBaoSerialSPchuaIn"] = "Có lỗi Insert Serial SP.";
                        break;
                    }
                }
                //tạo S/N Box
                for (int i = 0; i < SoSerialN; i++)
                {
                    SerSP.LoSanXuat = solobox;
                    string SN = Utils.XString.MakeAotuSN(10) + SerSP.LoSanXuat;
                    var kt = dbc.Ser_box.Where(kh => kh.Serial.Contains(SN)).ToList();
                    if (kt.Count() > 0)
                    {
                        //Nếu S/N bị trùng, random 50 lần
                        for (int j = 0; j < 50; j++)
                        {
                            SN = Utils.XString.MakeAotuSN(10) + SerSP.LoSanXuat;
                            var ktt = dbc.Ser_box.Where(kh => kh.Serial.Contains(SN)).ToList();
                            if (ktt.Count() == 0)
                            {
                                SerSP.SerialSP = SN;
                                break;
                            }
                        }
                    }
                    else
                    {
                        SerSP.SerialSP = SN;
                    }
                    SerSP.Stt = (i + 1).ToString();

                    var kq = new Data.SerialData().InsertSer_Box(SerSP.LoSanXuat,SerSP.SerialSP,SerSP.Sudung,SerSP.Stt,SerSP.Ghichu);
                    if (kq == false)
                    {
                        Session["ThongBaoSerialBoxchuaIn"] = "Có lỗi Insert Serial Box.";
                        break;
                    }
                }
                Session["ThongBaoSerialSPchuaIn"] = "Tạo mới thành công "+SoSerialN+" Serial SP.";
                Session["ThongBaoSerialBoxchuaIn"] = "Tạo mới thành công " + SoSerialN + " Serial Box.";
            }
            catch (Exception ex) {
                string loi = ex.ToString();
                ModelState.AddModelError("", "Thêm mới Thất Bại !!!!!!!!!!. Có lỗi hệ thống");
                return View();
            }
            return RedirectToAction("ListSerialChuaIn");
        }
        public ActionResult DeleteSerialSP()
        {
            try
            {
                var kt = dbc.Ser_sp.Where(kh=>kh.DaIn==false).ToList();
                if (kt.Count() == 0)
                {
                    Session["ThongBaoSerialSPchuaIn"] = "Không còn S/N SP nào chưa in để Xóa.";
                    return RedirectToAction("ListSerialChuaIn");
                }
                string sql = "DELETE  FROM [" + DBname + "TechZone].[dbo].[Ser_sp] where DaIn=0";
                var XoaChitietXN = dbc.Database.ExecuteSqlCommand(sql);
                Session["ThongBaoSerialSPchuaIn"] = "Xóa tất cả S/N SP thành công.";
                return RedirectToAction("ListSerialChuaIn");
            }
            catch (Exception ex) {
                string loi = ex.ToString();
                Session["ThongBaoSerialSPchuaIn"] = "Có Lỗi hệ thống khi xóa S/N SP !!!";
                return RedirectToAction("ListSerialChuaIn");
            }
        }
        public ActionResult DeleteSerialBox()
        {
            try
            {
                var kt = dbc.Ser_box.Where(kh => kh.DaIn == false).ToList();
                if (kt.Count() == 0)
                {
                    Session["ThongBaoSerialBoxchuaIn"] = "Không còn S/N Box nào chưa in để Xóa.";
                    return RedirectToAction("ListSerialChuaIn");
                }
                string sql = "DELETE  FROM [" + DBname + "TechZone].[dbo].[Ser_box] where DaIn=0";
                var XoaChitietXN = dbc.Database.ExecuteSqlCommand(sql);
                Session["ThongBaoSerialBoxchuaIn"] = "Xóa tất cả S/N Box thành công.";
                return RedirectToAction("ListSerialChuaIn");
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                Session["ThongBaoSerialBoxchuaIn"] = "Có Lỗi hệ thống khi xóa S/N Box !!!";
                return RedirectToAction("ListSerialChuaIn");
            }
        }
        public ActionResult GetListSer_SP()
        {
            ViewBag.SerSPchuaIn = dbc.Ser_sp.Where(kh => kh.Sudung == false).OrderBy(kh => kh.NgayTao).ToList();
            return PartialView();
        }
        public ActionResult GetListSer_Box()
        {
            ViewBag.SerBoxchuaIn = dbc.Ser_box.Where(kh => kh.Sudung == false).OrderBy(kh => kh.NgayTao).ToList();
            return PartialView();
        }
    }
}