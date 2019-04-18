using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult DangNhap()
        {
            if (Session["DangNhap"]!=null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        [HttpPost]

        public ActionResult DangNhap(User user)
        {
            User us1 = db.Users.SingleOrDefault(n => n.username == user.username && n.pwd == user.pwd);
            if(us1!=null)
            {
                Session["DangNhap"] = us1.username;
                Session["pwd"] = us1.pwd;
                Session["email"] = us1.email;
                Session["phone"] = us1.phone;
                Session["address"] = us1.address;
                Session["fullname"] = us1.fullname;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không chính xác!";
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session["DangNhap"] = null;
            Session["DangNhap"] = null;
            Session["User_id"] = null;
            Session["pwd"] = null;
            Session["email"] = null;
            Session["phone"] = null;
            Session["address"] = null;
            Session["fullname"] = null;
            return Redirect("DangNhap");
        }

        public ActionResult QuenMatKhau()
        {
            if (Session["DangNhap"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult QuenMatKhau(User user)
        {
            User us = db.Users.SingleOrDefault(n => n.username == user.username && n.email == user.email);
            if (us != null)
            {
                ViewBag.Resetpwd = "Mật khẩu mới của bạn là:'0'";
                us.pwd = "0";
                db.SaveChanges();
            }
            else
            {
                ViewBag.Resetpwd = "Sai tên tài khoản không trùng với email!";
            }

            return View();
        }

        public ActionResult DangKy()
        {
            if (Session["DangNhap"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Redirect("DangNhap");
        }

        public ActionResult Thongtincanhan()
        {
            if (Session["DangNhap"] == null)
            {
                return Redirect("DangNhap");
            }
            return View();
        }

        [HttpPost]

        public ActionResult Thongtincanhan(User user)
        {
            User us1 = db.Users.SingleOrDefault(n=>n.username==user.username);
            us1.pwd = user.pwd;
            us1.fullname = user.fullname;
            us1.email = user.email;
            us1.address = user.address;
            us1.phone = user.phone;
            Session["pwd"] = us1.pwd;
            Session["email"] = us1.email;
            Session["phone"] = us1.phone;
            Session["address"] = us1.address;
            Session["fullname"] = us1.fullname;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }


    }
}