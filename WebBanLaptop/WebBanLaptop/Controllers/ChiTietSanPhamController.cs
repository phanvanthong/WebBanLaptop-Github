using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        // GET: ChiTietSanPham
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult Index(int id = 0)
        {
            Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Hangsx = db.Hangsxes.Single(n => n.Hangsx_id == id).tenhang;
            return View(product);
        }
    }
}