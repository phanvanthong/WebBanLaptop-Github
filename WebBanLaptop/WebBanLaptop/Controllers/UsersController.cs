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
            Session["DangNhap"] = null;
            return View();
        }

        [HttpPost]

        public ActionResult DangNhap(User user)
        {
            User us1 = db.Users.SingleOrDefault(n => n.username == user.username && n.pwd == user.pwd);
            if(us1!=null)
            {
                Session["DangNhap"] = user.username;
                Session["User_id"] = user.Users_id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
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
        public ActionResult DangKy(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Redirect("Index");
        }

    }
}