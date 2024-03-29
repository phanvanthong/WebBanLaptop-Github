﻿using System;
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
            ViewBag.khuyenmai = ""+id+"";
            //ViewBag.Hangsx= db.Hangsxes.SingleOrDefault(n=>n.Hangsx_id)
            return View(db.Products.Where(n => n.Discount_id == id).ToList().OrderByDescending(n => n.Ngaytao).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult TimKiemLaptop(string hangsx, string gia, string manhinh, string khuyenmai, string RAM, string ocung, int page = 1)
        {
            ViewBag.hangsx = hangsx;
            ViewBag.gia = gia;
            ViewBag.manhinh = manhinh;
            ViewBag.khuyenmai = khuyenmai;
            ViewBag.RAM = RAM;
            ViewBag.ocung = ocung;
            string query = "exec DkLaptopUser '" + hangsx + "','" + gia + "','" + manhinh + "','" + khuyenmai + "','" + RAM + "','" + ocung + "'";
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