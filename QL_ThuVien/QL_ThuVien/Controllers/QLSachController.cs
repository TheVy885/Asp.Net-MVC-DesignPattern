using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;
using System.Data;
using System.IO;
using Logger;

namespace QL_ThuVien.Controllers
{
    public class QLSachController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();

        private ILog _ILog;
        public QLSachController()
        {
            _ILog = Log.GetInstance;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            _ILog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.View("Error").ExecuteResult(this.ControllerContext);
        }

        // GET: QLSach
        public ActionResult Index(string tensach)
        {
            if (tensach == null)
            {
                return View(data.Saches.ToList());
            }
            else
            {
                return View(data.Saches.Where(s => s.TenSach.Contains(tensach)).ToList());
            }
        }

        // GET: QLSach/Details/5
        public ActionResult Details(int id)
        {
            return View(data.Saches.Where(s => s.IDSach == id).FirstOrDefault());
        }

        // GET: QLSach/Create
        public ActionResult Create()
        {
            List<TheLoai> list = data.TheLoais.ToList();
            ViewBag.listcate = new SelectList(list, "IDCate", "NameCate", "");

            Sach sach = new Sach();
            return View(sach);
        }

        // POST: QLSach/Create
        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            try
            {
                List<TheLoai> list = data.TheLoais.ToList();

                if (sach.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sach.UploadImage.FileName);
                    string ex = Path.GetExtension(sach.UploadImage.FileName);
                    filename = filename + ex;
                    sach.HinhAnh = "~/Content/Image/" + filename;
                    sach.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), filename));

                }
                ViewBag.listcate = new SelectList(list, "IDCate", "NameCate", 1);
                data.Saches.Add(sach);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Vui lòng kiểm tra lại thông tin!');</script>");
            }
            
          
        }

        // GET: QLSach/Edit/5
        public ActionResult Edit(int id)
        {
            return View(data.Saches.Where(s => s.IDSach == id).FirstOrDefault());
        }

        // POST: QLSach/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Sach sach)
        {
            
                // TODO: Add update logic here
               // sach = data.Saches.Where(s => s.IDSach == id).FirstOrDefault();
                data.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("Index");
           
        }

        // GET: QLSach/Delete/5
       public ActionResult Delete(int id)
        {
            return View(data.Saches.Where(s=>s.IDSach == id).FirstOrDefault());
        }

        // POST: QLSach/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Sach sach)
        {
            try
            {
                // TODO: Add delete logic here
                sach = data.Saches.Where(s => s.IDSach == id).FirstOrDefault();
                data.Saches.Remove(sach);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
           {
                return Content("Bạn Không được xóa! Sách còn đang trong quá trình mượn");
           }
                
            
        }


        public ActionResult Duplicate(int id)
        {
            return View(data.Saches.Where(s => s.IDSach == id).FirstOrDefault());
        }

        // POST: QLSach/Duplicate/5
        [HttpPost]
        public ActionResult Duplicate(int id, Sach sach) //nhân bản sách
        {
            try
            {
                // TODO: Add delete logic here
                sach = data.Saches.Where(s => s.IDSach == id).FirstOrDefault();
                List<TheLoai> list = data.TheLoais.ToList();            
                var cloneSach = sach.Clone();

                ViewBag.listcate = new SelectList(list, "IDCate", "NameCate", 1);
                data.Saches.Add((Sach)cloneSach);
                data.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Vui lòng kiểm tra thông tin sách!');</script>");
            }


        }
        private bool SachExist(int id)
        {
            return data.Saches.Any(e => e.IDSach == id);
        }





    }
}
