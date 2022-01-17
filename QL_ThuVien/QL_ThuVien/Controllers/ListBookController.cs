using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;
using System.Data;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace QL_ThuVien.Controllers
{
    public class ListBookController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        // GET: ListBook


        public ActionResult TimKiem (string _name)
        {
            if (_name == null)
            {
                return View(data.Saches.ToList());
            }
            else
            {
                return View(data.Saches.Where(s => s.TenSach.Contains(_name)).ToList());
            }
        }


        public ActionResult Index(string category,int? page,double min = Double.MinValue,double max= Double.MaxValue)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);

            if (category == null)
            {
                var sach = data.Saches.OrderByDescending(x => x.TheLoai);
                return View(sach.ToPagedList(pageNum,pageSize));
            }
            else
            {
                var sach = data.Saches.OrderByDescending(x => x.TheLoai).Where(x => x.TheLoai == category);
                return View(sach);
            }
           
        }

        // GET: ListBook/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

      
       

        
    }
}
