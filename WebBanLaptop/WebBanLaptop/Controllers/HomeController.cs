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
        //public ActionResult Index(int ? page)
        //{
        //    int pageNumber = (page ?? 1);
        //    int pageSize = 12;
        //    //ViewBag.Hangsx= db.Hangsxes.SingleOrDefault(n=>n.Hangsx_id)
        //    return View(db.Products.ToList().OrderByDescending(n => n.Products_id).ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult Index()
        {
            //(int? page)
            //int pageNumber = (page ?? 1);
            //int pageSize = 12;
            //db.Products.OrderByDescending(n => n.Products_id).ToList().ToPagedList(pageNumber, pageSize)
            List<Product> lstproduct = db.Products.OrderByDescending(n => n.Ngaytao).Take(8).ToList();
            var discountMAX = db.Discounts.OrderBy(n=>n.value).FirstOrDefault();
            var productKM = db.Products.Where(n => n.Discount_id == discountMAX.Discount_id).Take(8).ToList();
            ViewBag.KM = productKM;
            return View(lstproduct);
        }

        public PartialViewResult PartialSanPham()
        {
            List<Hangsx> lsthangsx = db.Hangsxes.OrderBy(n => n.Hangsx_id).ToList();
            //var category = db.Hangsxes.ToList();
            //ViewBag.View = category;
            return PartialView(lsthangsx);
        }

        
        public PartialViewResult PartialKM()
        {
            List<Discount> lstdiscount = db.Discounts.OrderBy(n => n.Discount_id).ToList();
            return PartialView(lstdiscount);
        }

        public PartialViewResult PartialGioHang()
        {
            //List<Discount> lstdiscount = db.Discounts.OrderBy(n => n.Discount_id).ToList();
            List<Giohang> lstgiohang = new List<Giohang>();
            lstgiohang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
            if (lstgiohang == null)
            {
                lstgiohang = Session["GioHang"] as List<Giohang>;
            }
            if (lstgiohang == null)
            {
                     lstgiohang = new List<Giohang>();
            }
            
            return PartialView(lstgiohang);
        }

        //public PartialViewResult PartialDiaDiem()
        //{
        //    List<Discount> lstdiscount = db.Discounts.OrderBy(n => n.Discount_id).ToList();
        //    return PartialView(lstdiscount);
        //}
    }

  

}