using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult Index()
        {
            if(Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }

        public ActionResult DangNhap()
        {
            Session["DangNhapAdmin"] = null;
            return View();
        }

        [HttpPost]

        public ActionResult DangNhap(Admin admin)
        {
            Admin admin1 = db.Admins.SingleOrDefault(n => n.username == admin.username && n.pwd == admin.pwd);
            if (admin1 != null)
            {
                Session["DangNhapAdmin"] = admin.username;
                Session["Admin_id"] = admin.admin_id;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không chính xác!";
                return View();
            }
        }

        public ActionResult QuenMatKhau()
        {
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(Admin admin)
        {
            db.Admins.Add(admin);
            db.SaveChanges();
            return Redirect("DangNhap");
        }
    }
}