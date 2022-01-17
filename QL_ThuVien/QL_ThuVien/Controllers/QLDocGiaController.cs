using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;
using static QL_ThuVien.Models.DocGia;

namespace QL_ThuVien.Controllers
{
    public class QLDocGiaController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        // GET: QLDocGia
        public ActionResult Index(string _name)
        {
           
            if (_name == null)
            {
                return View(data.DocGias.ToList());
            }
            else
            {
                return View(data.DocGias.Where(s => s.TenDG.Contains(_name) ).ToList());
            }
            
            
        }

        // GET: QLDocGia/Details/5
        public ActionResult Details(string id)
        {
            return View(data.DocGias.Where(s => s.IDDG == id).FirstOrDefault());
        }

        // GET: QLDocGia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLDocGia/Create
        [HttpPost]
        public ActionResult Create( DocGia docGia)
        {
            var items = (from item in data.DocGias select item).ToList(); 

            Iterator iterator = new DocGiaIterator(items); 
            var i = iterator.First();
            while (!iterator.IsDone)
            {
                data.DocGias.Add(docGia);
                i = iterator.Next();
            }
            data.SaveChanges();
            return RedirectToAction("Index","QLDocGia");
               
        }

        // GET: QLDocGia/Edit/5
        public ActionResult Edit(string id)
        {
            return View(data.DocGias.Where(s=>s.IDDG == id).FirstOrDefault());
        }

        // POST: QLDocGia/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, DocGia docGia)
        {

            // TODO: Add update logic here
            data.Entry(docGia).State = System.Data.Entity.EntityState.Modified;
            //data.SaveChanges();
            //kh.UpdateDatabase(id, kh);

            Proxy proxy = new Proxy(docGia);
            CodeAppUser codeUpdate = proxy.UpdateDatabase(id,docGia); //lấy code 


            if (codeUpdate == CodeAppUser.InvalidFullName) // kiểm tra code nếu là invalid thì ko cho phép và báo lỗi
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Tên ko được phép');</script>");
            }
            else if (codeUpdate == CodeAppUser.InvalidPassword)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Password quá yếu');</script>");
            }
            else
            {
                data.SaveChanges();
                return RedirectToAction("Index");
            }
 
           
            
        }

        // GET: QLDocGia/Delete/5
        public ActionResult Delete(string id)
        {
            return View(data.DocGias.Where(s => s.IDDG == id).FirstOrDefault());
        }

        // POST: QLDocGia/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, DocGia kh)
        {
            try
            {
                // TODO: Add delete logic here
                kh = data.DocGias.Where(s => s.IDDG == id).FirstOrDefault();
                data.DocGias.Remove(kh);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Đọc giả còn đang mượn sách không được xóa !");
            }
        }
    }
}
