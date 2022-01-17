using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class HomeController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        public ActionResult Index()
        {
            return View(data.Saches.Take(5).OrderByDescending(s=>s.IDSach).ToList());
        }

        
    }
}