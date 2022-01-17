using QL_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_ThuVien.Controllers
{
    public class TraCuuPhieuMuonController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        // GET: TraCuuPhieuMuon

        public ActionResult Index()
        {
            return View(data.PhieuMuons.ToList());
        }

        public ActionResult Details(int id =0)
        {
            return View(data.PhieuMuons.Where(s => s.IDPM == id).FirstOrDefault());
        }

        public ActionResult TimKiemTheoID(string tendg )
        {
            
            var list = data.PhieuMuons.Where(p => p.TenDG.Contains(tendg)).ToList();
            return View(list);

        }
    }
}