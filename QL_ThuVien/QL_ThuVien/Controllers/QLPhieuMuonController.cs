using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class QLPhieuMuonController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        // GET: QLPhieuMuon
        public ActionResult Index(string _name)
        {

            if (_name == null)
            {
                return View(data.PhieuMuons.ToList());
            }
            else
            {
                return View(data.PhieuMuons.Where(s => s.TenDG.Contains(_name)).ToList());
            }


        }

        // GET: QLPhieuMuon/Details/5
        public ActionResult Details(int id)
        {
            return View(data.PhieuMuons.Where(s=>s.IDPM == id).FirstOrDefault());
        }


        // GET: QLPhieuMuon/Edit/5
        public ActionResult Edit(int id)
        {
            return View(data.PhieuMuons.Where(s => s.IDPM == id).FirstOrDefault());
        }

        // POST: QLPhieuMuon/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PhieuMuon p)
        {

            // TODO: Add update logic here
            data.Entry(p).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
            return RedirectToAction("Index");
           
        }

        // GET: QLPhieuMuon/Edit/5
        public ActionResult Phat(int id)
        {
            return View(data.PhieuMuons.Where(s => s.IDPM == id).FirstOrDefault());
        }

        // POST: QLPhieuMuon/Edit/5
        [HttpPost]
        public ActionResult Phat(int id, PhieuMuon p)
        {

            // TODO: Add update logic here
            data.Entry(p).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: QLPhieuMuon/Delete/5
        public ActionResult Delete(int id)
        {
            return View(data.PhieuMuons.Where(s => s.IDPM == id).FirstOrDefault());
        }

        // POST: QLPhieuMuon/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PhieuMuon p)
        {
            try
            {
                // TODO: Add delete logic here
                p = data.PhieuMuons.Where(s => s.IDPM == id).FirstOrDefault();
                if (p.TienPhat != 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Độc giả chưa đóng tiền phạt thì không được xóa phiếu mượn!');</script>");
                }
                data.PhieuMuons.Remove(p);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
           {
                return Content("Bạn Không được xóa! Sách vẫn chưa được trả vui lòng kiểm tra lại.....");
            }
        }

        
    }
}
