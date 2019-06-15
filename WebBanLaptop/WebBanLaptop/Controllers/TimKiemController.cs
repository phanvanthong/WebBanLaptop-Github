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

        //[HttpPost]
        //public ActionResult Index(FormCollection f, int page = 1)
        //{
        //    string TuKhoa = f["txtTimKiem"].ToString();
        //    ViewBag.TuKhoa = TuKhoa;
        //    Session["TuKhoa"] = TuKhoa;
        //    List<Product> lstKQTK = db.Products.Where(n => n.Name.Contains(TuKhoa)).ToList();
        //    string query = "select *from products where name like '%" + TuKhoa + "%'";
        //    List<Product> lstKQTK = db.Database.SqlQuery<Product>(query).ToList<Product>();
        //    if (lstKQTK.Count == 0)
        //    {
        //        ViewBag.TimKiem = "Không tìm thấy kết quả nào";
        //    }
        //    else
        //    {
        //        ViewBag.TimKiem = "Tìm thấy " + lstKQTK.Count + " kết quả";
        //    }
        //    int pageNumber = page;
        //    int pageSize = 12;
        //    return View(db.Database.SqlQuery<Product>(query).ToList<Product>().ToPagedList(pageNumber, pageSize));
        //}

        [HttpGet]

        public ActionResult Index(string Tukhoa, int page = 1)
        {
            ViewBag.TuKhoa = Tukhoa;
            Session["TuKhoa"] = Tukhoa;
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

        public ActionResult TimKiemLaptop(string hangsx, string gia, string manhinh, string khuyenmai, string RAM, string ocung, int page = 1)
        {
            ViewBag.TuKhoa = Session["TuKhoa"];
            ViewBag.hangsx = hangsx;
            ViewBag.gia = gia;
            ViewBag.manhinh = manhinh;
            ViewBag.khuyenmai = khuyenmai;
            ViewBag.RAM = RAM;
            ViewBag.ocung = ocung;
            string query = "exec DkLaptop '" + Session["TuKhoa"] + "','" + hangsx + "','" + gia + "','" + manhinh + "','" + khuyenmai + "','" + RAM + "','" + ocung + "'";
            List<Product> lstKQTK = db.Database.SqlQuery<Product>(query).ToList<Product>();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiemLaptop = "Không tìm thấy kết quả nào!";
            }
            else
            {
                ViewBag.TimKiemLaptop = "Tìm thấy " + lstKQTK.Count + " kết quả!";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Database.SqlQuery<Product>(query).ToList<Product>().ToPagedList(pageNumber, pageSize));

        }

    }
}