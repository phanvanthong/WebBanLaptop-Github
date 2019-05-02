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

        [HttpPost]
        public ActionResult Index(FormCollection f, int page=1)
        {
            string TuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = TuKhoa;
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
            return View(db.Products.Where(n => n.Name.Contains(TuKhoa)).OrderByDescending(n=>n.Ngaytao).ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]

        public ActionResult Index(string Tukhoa, int page = 1)
        {
            ViewBag.TuKhoa = Tukhoa;
            List<Product> lstKQTK = db.Products.Where(n => n.Name.Contains(Tukhoa)).ToList();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiem = "Không tìm thấy kết quả nào";
            }
            else
            {
                ViewBag.TimKiem = "Tìm thấy " + lstKQTK.Count + " kết quả";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Products.Where(n => n.Name.Contains(Tukhoa)).OrderByDescending(n => n.Ngaytao).ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}