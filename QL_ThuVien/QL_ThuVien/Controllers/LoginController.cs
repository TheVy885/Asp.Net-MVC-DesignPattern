using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class LoginController : Controller
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAcc(DocGia user)
        {

            var check = data.DocGias.Where(s => s.NameUser == user.NameUser && s.Password == user.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.Alert = "abc";
                ViewBag.ErrorInfo = "Sai Info";
                
                return View("Index");
            }
            else
            {
                data.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = user.NameUser;
                Session["Password"] = user.Password;
                return RedirectToAction("Index", "ListBook");
            }

        }

        public ActionResult IndexAdmin ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin (DangNhap admin)
        {
            var check = data.DangNhaps.Where(s => s.NameUser == admin.NameUser && s.Password == admin.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info";
                return View("Index");
            }
            else
            {
                data.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = admin.NameUser;
                Session["Password"] = admin.Password;
                return RedirectToAction("Index", "QLDocGia");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(DocGia user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var check = data.DocGias.Where(s => s.IDDG == user.IDDG).FirstOrDefault();
                    if (check == null)
                    {
                        data.Configuration.ValidateOnSaveEnabled = false;
                        data.DocGias.Add(user);
                        data.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorRegister = "this id is exist";
                        return View();
                    }

                }
            }
            catch
            {
                ViewBag.ErrorRegister = "Vui lòng điền đầy đủ và kiểm tra lại thông tin !";
            }
            return View();


        }
    }
}
