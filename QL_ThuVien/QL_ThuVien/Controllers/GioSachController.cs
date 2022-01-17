using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class GioSachController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();

        

        public GioSach GetSach()
        {
            GioSach gio = Session["GioSach"] as GioSach;
            if (gio == null || Session["GioSach"] == null)
            {
                gio = new GioSach();
                Session["GioSach"] = gio;
            }
            return gio;
        }

        // GET: GioSach
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Addto (int id)
        {
            var gio = data.Saches.SingleOrDefault(s => s.IDSach == id);
            if (gio != null)
            {
                GetSach().Add(gio);
            }
            return RedirectToAction("Show", "GioSach");
        }

        public ActionResult Show ()
        {
            if (Session["GioSach"] == null)
            {
                return RedirectToAction("Show", "GioSach");              
            }
            GioSach gio = Session["GioSach"] as GioSach;
            return View(gio);
        }

        public ActionResult Update_quantity (FormCollection form)
        {
            GioSach gio = Session["GioSach"] as GioSach;
            int id_sach = int.Parse(form["Id sach"]);
            int quantity = int.Parse(form["quantity"]);
            gio.Update(id_sach, quantity);
            return RedirectToAction("Show", "GioSach");
        }

        public ActionResult Remove (int id)
        {
            GioSach gio = Session["GioSach"] as GioSach;
            gio.Remove(id);
            return RedirectToAction("Show", "GioSach");
        }

        public PartialViewResult BagBook ()
        {
            int total = 0;
            GioSach gio = Session["GioSach"] as GioSach;
            if (gio != null)
            {
                total = gio.Total();
            }
            
            ViewBag.infocart = total;
            return PartialView("BagBook");
        }

        public ActionResult Muon_Success ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MuonSach(FormCollection form)
        {
            try
            {
                GioSach gio = Session["GioSach"] as GioSach;
                PhieuMuon muon = new PhieuMuon();

                muon.TenDG = form["Tendg"];
                muon.NgayMuon = DateTime.Now;
                muon.IDDG = form["IDdocgia"];
                muon.TienPhat = 0;
                muon.GhiChu = "";
                muon.NgayTra = DateTime.Parse(form["NgayTra"]);

                DateTime ngaymuon = Convert.ToDateTime(muon.NgayMuon);
                DateTime ngaytra = Convert.ToDateTime(muon.NgayTra);

                TimeSpan Time = ngaytra - ngaymuon;
                int TongSoNgay = Time.Days;

                if (TongSoNgay > 15)
                {
                    //return Content("Lỗi ! Thời gian mượn tối đa là 15 ngày");
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Lỗi ! Thời gian mượn tối đa là 15 ngày');</script>");
                }
                if (TongSoNgay <= 0)
                {
                    //return Content("Lỗi ! Vui lòng kiểm tra lại mốc thời gian");
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Lỗi ! Vui lòng kiểm tra lại mốc thời gian');</script>");
                }

                int total = gio.Total();
                if (total > 3)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Tối đa được mượn 3 loại/quyển sách');</script>");
                    //return Content("Tối đa được mượn 3 loại sách");
                }





                data.PhieuMuons.Add(muon);

                foreach (var item in gio.Item)
                {
                    int tongsach = 0;
                    tongsach = tongsach + 1;


                    if (item._soluongSach == 0)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert     ('Vui lòng kiểm tra số lượng! Do có sách không còn đủ số lượng');</script>");
                        //return Content("Vui lòng kiểm tra số lượng! Do có sách không còn đủ số lượng");
                        
                    };
                    if (tongsach == 0)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert     ('Không có sách thì không thể tạo phiếu mượn!');</script>");
                    }
                    
                    CT_PM Detail = new CT_PM();
                    Detail.IDPM = muon.IDPM;
                    Detail.IDSach = item.giosach.IDSach;
                    Detail.TenSach = item.giosach.TenSach;
                    Detail.IDDG = muon.IDDG;
                    Detail.TenDG = muon.TenDG;
                    Detail.SoLuong = item._soluongSach;
                    Detail.TrangThai = "Đang Mượn";

                    data.CT_PM.Add(Detail);

                    foreach (var ct in data.CT_PM.Where(s => s.IDDG == muon.IDDG))
                    {
                        if (ct.TrangThai != "Đã Trả")
                        {
                            //return Content("Độc giả chưa trả sách thì không được mượn thêm sách!");
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Độc giả chưa trả sách thì không được mượn thêm sách!');</script>");
                        }
                    }

                    foreach (var pm in data.PhieuMuons.Where(s => s.IDDG == muon.IDDG))
                    {
                        if (pm.TienPhat != 0)
                        {
                            //return Content("Độc giả chưa đóng tiền phạt thì không được mượn thêm sách!");
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Độc giả chưa đóng tiền phạt thì không được mượn thêm sách!');</script>");
                        }
                    }

                    foreach (var p in data.Saches.Where(s => s.IDSach == Detail.IDSach))
                    {
                        var update_soluong = p.SoLuong - item._soluongSach;
                        p.SoLuong = update_soluong;
                    }
                    foreach (var p in data.Saches.Where(s => s.IDSach == Detail.IDSach))
                    {
                        if (p.SoLuong < item._soluongSach)
                        {
                            /* return Content("Không đủ sách theo yêu cầu của Độc Giả!");*/
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Không đủ sách theo yêu cầu của Độc Giả!');</script>");
                        }
                    }
                }
                data.SaveChanges();
                gio.claer();
                return RedirectToAction("Muon_Success", "GioSach");
            }
            catch
            {
                return Content("Vui lòng kiểm tra lại thông tin!");
            }
        }

    }
}
