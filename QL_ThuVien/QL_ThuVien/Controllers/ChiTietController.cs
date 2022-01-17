using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class ChiTietController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        // GET: ChiTiet
        public ActionResult Index(string tendg)
        {
            if (tendg == null)
            {
                return View(data.CT_PM.ToList());
            }
            else
            {
                return View(data.CT_PM.Where(s => s.TenDG.Contains(tendg)).ToList());
            }
        }

        // GET: ChiTiet/Details/5
        public ActionResult Details(int id)
        {
            return View(data.CT_PM.Where(s=>s.ID== id).FirstOrDefault());
        }

        // GET: ChiTiet/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ChiTiet/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ChiTiet/Edit/5
        public ActionResult Edit(int id)
        {
            List<TrangThai> list = data.TrangThais.ToList();
            ViewBag.listcate = new SelectList(list, "IdTT", "TT", "");
            return View(data.CT_PM.Where(s => s.ID == id).FirstOrDefault());
        }

        // POST: ChiTiet/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CT_PM ct)
        {

            List<TrangThai> list = data.TrangThais.ToList();
            ViewBag.listcate = new SelectList(list, "IdTT", "TT", 1);

            //test xu ly 
            

            foreach (var sach in data.Saches.Where(s => s.IDSach == ct.IDSach))
            {
                var update_soluong = sach.SoLuong +ct.SoLuong;
                sach.SoLuong = update_soluong;
            }
            
            

            //code mac dinh 
            data.Entry(ct).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: ChiTiet/Delete/5
        public ActionResult Delete(int id)
        {
            return View(data.CT_PM.Where(s => s.ID == id).FirstOrDefault());
        }

       
        // POST: ChiTiet/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CT_PM ct)
        {
            try
            {
                
                // TODO: Add delete logic here
                ct = data.CT_PM.Where(s => s.ID == id).FirstOrDefault();

                
                

                /*if (String.Compare(ctpm.TrangThai,trangthaihientai,true)==0)
                {
                    return Content("Lỗi");
                }*/
                if(ct.NgayTraThucTe==null)
                {
                    return Content("Lỗi! Chưa có ngày trả sách thực tế!");
                }
                if(ct.TrangThai!="Đã Trả")
                {
                    return Content("Độc giả chưa trả sách nên không thế xóa phiếu mượn!");
                }    

                data.CT_PM.Remove(ct);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SearchTheoTenDG(string tendg)
        {

            var list = data.CT_PM.Where(p => p.TenDG.Contains(tendg)).ToList();
            return View(list);
        }
    }
}
