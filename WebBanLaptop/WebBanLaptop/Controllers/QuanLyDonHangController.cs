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
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult DonHangChuaXacNhan()
        {
            
            return View();
        }

        public ActionResult DonHangDaXacNhan()
        {
            return View();
        }

        public ActionResult DonHangThanhCong()
        {
            return View();
        }
    }
}