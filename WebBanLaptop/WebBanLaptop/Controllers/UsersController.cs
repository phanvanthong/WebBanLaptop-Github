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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangKyUser()
        {
            return View();
        }

        public ActionResult QuenMatKhau()
        {
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }

        
    }
}