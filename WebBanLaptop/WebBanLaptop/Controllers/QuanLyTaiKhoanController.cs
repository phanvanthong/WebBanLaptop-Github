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

        public ActionResult TimKiemAdmin(string TuKhoaAd, int page = 1)
        {
            ViewBag.TuKhoaAd = TuKhoaAd;
            string query = "select *from Admin where username like '%" + TuKhoaAd + "%'";
            List<Admin> lstKQTK = db.Database.SqlQuery<Admin>(query).ToList<Admin>();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiemAdmin = "Không tìm thấy kết quả nào!";
            }
            else
            {
                ViewBag.TimKiemAdmin = "Tìm thấy " + lstKQTK.Count + " kết quả!";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Database.SqlQuery<Admin>(query).ToList<Admin>().ToPagedList(pageNumber, pageSize));

        }

        public ActionResult TimKiemUser(string TuKhoaUs, int page = 1)
        {
            ViewBag.TuKhoaUs = TuKhoaUs;
            string query = "select *from Users where username like '%" + TuKhoaUs + "%'";
            List<User> lstKQTK = db.Database.SqlQuery<User>(query).ToList<User>();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiemUser = "Không tìm thấy kết quả nào!";
            }
            else
            {
                ViewBag.TimKiemUser = "Tìm thấy " + lstKQTK.Count + " kết quả!";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Database.SqlQuery<User>(query).ToList<User>().ToPagedList(pageNumber, pageSize));

        }

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
            Admin ad = db.Admins.SingleOrDefault(n => n.username == admin.username);
            if(ad!=null)
            {
                ViewBag.DangKy = "Tên đăng nhập trùng!";
                return View();
            }
            else
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return Redirect("LstAdmin");
            }
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
        public ActionResult XacNhanChinhSuaAdmin(Admin admin)
        {
            Admin admin1 = db.Admins.SingleOrDefault(n => n.admin_id == admin.admin_id);
            
            if (ModelState.IsValid)
            {
                //db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
                admin1.fullname = admin.fullname;
                admin1.email = admin.email;
                admin1.level_ = admin.level_;
                admin1.phone = admin.phone;
                admin1.address = admin.address;
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
            User user1 = db.Users.SingleOrDefault(n => n.Users_id == user.Users_id);
            if (ModelState.IsValid)
            {
                //db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                user1.address = user.address;
                user1.email = user.email;
                user1.fullname = user.fullname;
                user1.phone = user.phone;
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