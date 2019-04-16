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
    public class HomeController : Controller
    {
        // GET: Home
        //QuanLyBanLaptopModel db = new QuanLyBanLaptopModel();
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult Index(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            //ViewBag.Hangsx= db.Hangsxes.SingleOrDefault(n=>n.Hangsx_id)
            return View(db.Products.ToList().OrderByDescending(n => n.Products_id).ToPagedList(pageNumber, pageSize));
        }
        //[ChildActionOnly]
        public PartialViewResult PartialSanPham()
        {
            List<Hangsx> lsthangsx = db.Hangsxes.OrderBy(n => n.Hangsx_id).ToList();
            //var category = db.Hangsxes.ToList();
            //ViewBag.View = category;
            return PartialView(lsthangsx);
        }

        
        //[ChildActionOnly]
        public PartialViewResult PartialKM()
        {
            List<Discount> lstdiscount = db.Discounts.OrderBy(n => n.Discount_id).ToList();
            return PartialView(lstdiscount);
        }



    }

  

}