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
            Hangsx hangsx = db.Hangsxes.SingleOrDefault(n => n.Hangsx_id == product.Hangsx_id);
            ViewBag.Hangsx = hangsx.tenhang;
            return View(product);
        }
    }
}