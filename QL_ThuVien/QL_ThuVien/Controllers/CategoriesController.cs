using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;
using Logger;

namespace QL_ThuVien.Controllers
{
    public class CategoriesController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();

        
        private ILog _ILog;
        public CategoriesController()
        {
            _ILog = Log.GetInstance;


            CategorySingleton.Instance.Init();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            _ILog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.View("Error").ExecuteResult(this.ControllerContext);
        }
        
        // GET: Categories
        public ActionResult Index()
        {
            
            var items = CategorySingleton.Instance.listCategory;
            
            return View(items.OrderBy(s=>s.ID));
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            return View(data.TheLoais.Where(s => s.ID == id).FirstOrDefault());
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TheLoai theLoai)
        {
            // TODO: Add insert logic here


            try
            {             
                data.TheLoais.Add(theLoai);
                data.SaveChanges();          
                CategorySingleton.Instance.Update();
               
                return RedirectToAction("Index");
            }
            catch
            {
              return Content("<script language='javascript' type='text/javascript'>alert     ('Vui lòng kiểm tra thông tin');</script>");
            }

        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            return View(data.TheLoais.Where(s => s.ID == id).FirstOrDefault());
        }

        // POST: Categories/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TheLoai theLoai)
        {
            data.Entry(theLoai).State = System.Data.Entity.EntityState.Modified;
            var listcategory = CategorySingleton.Instance.listCategory;
            data.SaveChanges();
           
            CategorySingleton.Instance.Update();
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            return View(data.TheLoais.Where(s=>s.ID == id).FirstOrDefault());
        }

        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TheLoai theLoai)
        {
           
                // TODO: Add delete logic here
                theLoai = data.TheLoais.Where(s => s.ID == id).FirstOrDefault();
                data.TheLoais.Remove(theLoai);
               
                var listcategory = CategorySingleton.Instance.listCategory;
                data.SaveChanges();
            CategorySingleton.Instance.Update();
                return RedirectToAction("Index");
           
        }

        /*
        public ActionResult Duplicate(int id)
        {
            return View(data.TheLoais.Where(s => s.ID == id).FirstOrDefault());
        }

        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Duplicate(int id, TheLoai theLoai)
        {
            
                // TODO: Add delete logic here
                theLoai = data.TheLoais.Where(s => s.ID == id).FirstOrDefault();
            //data.TheLoais.Remove(theLoai);
            //data.SaveChanges();

            data.TheLoais.Add(theLoai);
            data.SaveChanges();

            return RedirectToAction("Index");
           
        }*/








        public PartialViewResult CategoryPartial()
        {
            var catelist = data.TheLoais.ToList();
            return PartialView(catelist);
        }
    }
}
