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
            if(Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }

        public ActionResult DangNhap()
        {
            if(Session["Admin"]!=null)
            {
                return Redirect("Index");
            }
            return View();
        }

        [HttpPost]

        public ActionResult DangNhap(Admin admin)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username && n.pwd == admin.pwd);
            if (ad != null)
            {
                Session["Admin"] = ad as Admin;
                Session["AdminName"] = ad.username;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không chính xác!";
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            Session["AdminName"] = null;
            return Redirect("DangNhap");
        }

        public ActionResult QuenMatKhau()
        {
            if (Session["Admin"] != null)
            {
                return Redirect("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult QuenMatKhau(Admin admin)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username && n.email == admin.email);
            if (ad != null)
            {
                return RedirectToAction("ResetPwd", "Admin", ad);
            }
            else
            {
                ViewBag.Resetpwd = "Sai tên tài khoản không trùng với email!";
            }

            return View();
        }

        public ActionResult ResetPwd(Admin admin)
        {
            if (admin.username == null)
            {
                return Redirect("DangNhap");
            }
            return View(admin);
        }
        [HttpPost, ActionName("ResetPwd")]
        public ActionResult ResetPwdPost(Admin admin, string pwdnew, string pwdconfirm)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username);
            if (pwdnew != pwdconfirm)
            {
                ViewBag.Resetpwd = "Mật khẩu không trùng khớp!";
            }
            else
            {
                ViewBag.Resetpwd = "Đổi mật khẩu thành công";
                ad.pwd = pwdnew;
                db.SaveChanges();
            }
            return View(ad);
        }

        public ActionResult ThayDoiMK()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThayDoiMK(Admin admin, string pwdnew, string pwdconfirm)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username);
            if (pwdnew != pwdconfirm)
            {
                @ViewBag.thaydoimk = "Mật khẩu không trung khớp!";
            }
            else
            {
                ad.pwd = pwdnew;
                @ViewBag.thaydoimk = "Thay đổi mật khẩu thành công!";
                db.SaveChanges();
            }

            return View(ad);
        }

        public ActionResult DangKy()
        {
            if (Session["Admin"] == null)
            {
                return Redirect("DangNhap");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(Admin admin, string pwdconfirm)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username);
            if (ad != null)
            {
                ViewBag.DangKy = "Tên đăng nhập đã tồn tại!";
                return View();
            }
            else if (admin.pwd != pwdconfirm)
            {
                ViewBag.DangKy = "Đăng ký không thành công. Mật khẩu không trùng khớp!";
                return View();
            }
            db.Admins.Add(admin);
            db.SaveChanges();
            ViewBag.DangKy = "Đăng ký thành công!";
            return View();
        }

        public ActionResult Thongtincanhan()
        {
            if (Session["Admin"] == null)
            {
                return Redirect("DangNhap");
            }
            return View();
        }

        [HttpPost]

        public ActionResult Thongtincanhan(Admin admin)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username);
            ad.fullname = admin.fullname;
            ad.email = admin.email;
            ad.address = admin.address;
            ad.phone = admin.phone;
            db.SaveChanges();
            Session["Admin"] = ad;
            ViewBag.Thongtincanhan = "Thay đổi thành công!";
            return View();
        }
    }
}