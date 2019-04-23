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
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult Index(FormCollection f, int page=1)
        {
            string TuKhoa = f["txtTimKiem"].ToString();
            List<Product> lstKQTK = db.Products.Where(n => n.Name.Contains(TuKhoa)).ToList();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiem = "Không tìm thấy kết quả nào";
            }
            else
            {
                ViewBag.TimKiem = "Tìm thấy "+lstKQTK.Count+" kết quả";
            }
            int pageNumber = page ;
            int pageSize = 12;
            return View(db.Products.Where(n => n.Name.Contains(TuKhoa)).ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}