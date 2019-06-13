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
        //HttpCookie httpCookie = new HttpCookie("UserInfo");
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult DangNhap()
        {
            if (Session["User"] !=null)
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
                //httpCookie["Username"] = us1.Users_id.ToString();
                //httpCookie["Pwd"] = us1.pwd.ToString();
                //httpCookie.Expires.AddDays(3); //số ngày tồn tại cookie 
                //Response.Cookies.Add(httpCookie);
                Session["User"] = us1 as User;
                Session["DangNhap"] = us1.username;
                if (Session["rmbUser"] == "true")
                {
                    Session["rmbUserName"] = us1.username;
                    Session["rmbUserPwd"] = us1.pwd;
                }
                else
                {
                    Session["rmbUserName"] = null;
                    Session["rmbUserPwd"] = null;
                }
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
            Session["User"] = null;
            Session["DangNhap"] = null;
            return Redirect("DangNhap");
        }

        public ActionResult QuenMatKhau()
        {
            if (Session["User"] != null)
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
                return RedirectToAction("ResetPwd","Users",us);
            }
            else
            {
                ViewBag.Resetpwd = "Sai tên tài khoản không trùng với email!";
            }

            return View();
        }

        public ActionResult ResetPwd(User user)
        {
            if(user.username==null)
            {
                return Redirect("DangNhap");
            }
            return View(user);
        }
        [HttpPost, ActionName("ResetPwd")]
        public ActionResult ResetPwdPost(User user,string pwdnew,string pwdconfirm)
        {
            User us1 = db.Users.SingleOrDefault(n => n.username == user.username);
            if (pwdnew != pwdconfirm)
            {
                ViewBag.Resetpwd = "Mật khẩu không trùng khớp!";
            }
            else
            {
                ViewBag.Resetpwd = "Đổi mật khẩu thành công";
                us1.pwd = pwdnew;
                db.SaveChanges();
            }          
            return View(us1);
        }

        public ActionResult ThayDoiMK()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "Users");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThayDoiMK(User user,string pwdnew, string pwdconfirm)
        {
            User us = db.Users.SingleOrDefault(n => n.username == user.username);
            if (pwdnew !=pwdconfirm)
            {
                @ViewBag.thaydoimk = "Mật khẩu không trung khớp!";
            }
            else 
            {
                us.pwd = pwdnew;
                @ViewBag.thaydoimk = "Thay đổi mật khẩu thành công!";
                db.SaveChanges();
            }

            return View(user);
        }

        

        public ActionResult DangKy()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(User user,string pwdconfirm)
        {
            User us = db.Users.SingleOrDefault(n => n.username == user.username);
            if(us!=null)
            {
                ViewBag.DangKy = "Tên đăng nhập đã tồn tại!";
                return View();
            }
            else if(user.pwd != pwdconfirm)
            {
                ViewBag.DangKy = "Đăng ký không thành công. Mật khẩu không trùng khớp!";
                return View();
            }
            db.Users.Add(user);
            db.SaveChanges();
            ViewBag.DangKy = "Đăng ký thành công!";
            return View();
        }

        public ActionResult Thongtincanhan()
        {
            if (Session["User"] == null)
            {
                return Redirect("DangNhap");
            }
            return View();
        }

        [HttpPost]

        public ActionResult Thongtincanhan(User user)
        {
            User us1 = db.Users.SingleOrDefault(n=>n.username==user.username);
            us1.fullname = user.fullname;
            us1.email = user.email;
            us1.address = user.address;
            us1.phone = user.phone;
            db.SaveChanges();
            Session["User"] = us1;
            ViewBag.Thongtincanhan = "Thay đổi thành công!";
            return View();
        }

        public ActionResult rmbLogin(string cartModel)
        {
            if (cartModel == "0")
            {
                Session["rmbUser"] = "false";
            }
            else
            {
                Session["rmbUser"] = "true";
            }
            return Json(new
            {
                status = true
            });
        }


    }
}