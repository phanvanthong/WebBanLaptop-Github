using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanLaptop.Controllers
{
    public class PhukienController : Controller
    {
        // GET: Phukien
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RAM()
        {
            return View();
        }

        public ActionResult SSD()
        {
            return View();
        }

        public ActionResult HDD()
        {
            return View();
        }
    }
}