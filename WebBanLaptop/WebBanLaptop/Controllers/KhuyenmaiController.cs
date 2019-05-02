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
    public class KhuyenmaiController : Controller
    {
        // GET: Khuyenmai
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult Index(int id=1,int page=1)
        {
            int pageNumber = page;
            int pageSize = 12;
            //ViewBag.Hangsx= db.Hangsxes.SingleOrDefault(n=>n.Hangsx_id)
            return View(db.Products.Where(n => n.Discount_id == id).ToList().OrderByDescending(n => n.Ngaytao).ToPagedList(pageNumber, pageSize));
        }
    }
}