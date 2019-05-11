using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using PagedList;
using PagedList.Mvc;


namespace WebBanLaptop.Controllers
{
    public class QuanLyTaiKhoanController : Controller
    {
        // GET: QuanLyTaiKhoan
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult LstAdmin(int page=1)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Admins.ToList().OrderBy(n => n.admin_id).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiAdmin()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiAdmin(Admin admin)
        {
            db.Admins.Add(admin);
            db.SaveChanges();
            return Redirect("LstAdmin");
        }

        //----------

        public ActionResult ChinhSuaAdmin(int id = 0)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Admin admin = db.Admins.SingleOrDefault(n => n.admin_id == id);
            if (admin == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(admin);
        }
        [HttpPost, ActionName("ChinhSuaAdmin")]
        public ActionResult XacNhanChinhSuaAdmin(Admin Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Admin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("LstAdmin");
        }

        //----------

        public ActionResult XoaAdmin(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Admin admin = db.Admins.SingleOrDefault(n => n.admin_id == id);
            if (admin == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(admin);
        }

        [HttpPost, ActionName("XoaAdmin")]
        public ActionResult XacNhanXoaAdmin(int id)
        {
            Admin admin = db.Admins.SingleOrDefault(n => n.admin_id == id);
            if (admin == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("LstAdmin");
        }

        //----------

        public ActionResult HienThiAdmin(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Admin admin = db.Admins.SingleOrDefault(n => n.admin_id == id);
            if (admin == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(admin);
        }

        //------
        public ActionResult LstUser(int page = 1)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Users.ToList().OrderBy(n => n.Users_id).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiUser()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Redirect("LstUser");
        }

        //----------

        public ActionResult ChinhSuaUser(int id = 0)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            User user = db.Users.SingleOrDefault(n => n.Users_id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }
        [HttpPost, ActionName("ChinhSuaUser")]
        public ActionResult XacNhanChinhSuaUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("LstUser");
        }

        //----------

        public ActionResult XoaUser(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            User user = db.Users.SingleOrDefault(n => n.Users_id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

        [HttpPost, ActionName("XoaUser")]
        public ActionResult XacNhanXoaUser(int id)
        {
            User user = db.Users.SingleOrDefault(n => n.Users_id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("LstUser");
        }

        //----------

        public ActionResult HienThiUser(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            User user = db.Users.SingleOrDefault(n => n.Users_id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

    }
}