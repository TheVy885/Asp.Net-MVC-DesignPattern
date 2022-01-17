using QL_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class ThongKeController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        
        public ActionResult GroupByTop5()
        {
            List<CT_PM> ctpm = data.CT_PM.ToList();
            List<Sach> dsSach = data.Saches.ToList();
            var query = from ct in ctpm
                        join ds in dsSach on ct.IDSach equals ds.IDSach into tbl
                        group ct by new
                        {
                             idSach = Convert.ToString(ct.IDSach),
                            tenSach = ct.TenSach,
                            hinhanh = ct.Sach.HinhAnh,
                            theLoai = ct.Sach.TheLoai,
                            mota = ct.Sach.MoTa,
                            tacgia = ct.Sach.TacGia,

                        }
                        into gr
                        orderby gr.Sum(s => s.SoLuong) descending
                        select new ViewModel
                        {
                            IDSach = gr.Key.idSach,
                            TenSach = gr.Key.tenSach,
                            HinhAnh = gr.Key.hinhanh,
                            TheLoai = gr.Key.theLoai,
                            MoTa = gr.Key.mota,
                            TacGia = gr.Key.tacgia,
                            TongSoLuong = gr.Sum(s => s.SoLuong)
                        };
            return View(query.Take(5).ToList());

        }

        public ActionResult ThongKe()
        {
            List<CT_PM> ctpm = data.CT_PM.ToList();
            List<Sach> dsSach = data.Saches.ToList();
            var query = from ct in ctpm
                        join ds in dsSach on ct.IDSach equals ds.IDSach into tbl
                        group ct by new
                        {
                            idSach = Convert.ToString(ct.IDSach),
                            tenSach = ct.TenSach,
                            hinhanh = ct.Sach.HinhAnh,
                            theLoai = ct.Sach.TheLoai,
                            
                            tacgia = ct.Sach.TacGia,

                        }
                        into gr
                        orderby gr.Sum(s => s.SoLuong) descending
                        select new ViewModel
                        {
                            IDSach = gr.Key.idSach,
                            TenSach = gr.Key.tenSach,
                            HinhAnh = gr.Key.hinhanh,
                            TheLoai = gr.Key.theLoai,
                            
                            TacGia = gr.Key.tacgia,
                            TongSoLuong = gr.Sum(s => s.SoLuong)
                        };
            return View(query.ToList());

        }
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }
    }
}